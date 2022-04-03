using SpaceFighter.Control;
using SpaceFighter.Core;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] GameObject _gameOverScreen;
        [SerializeField] GameObject _gamePausedScreen;
        [SerializeField] GameObject _mobileJoystick;

        private Health _playerHealth;
        private GameObject _gamePausedScreenInstance;
        private bool _gameIsPaused;

        private void Awake()
        {
            this._playerHealth = this._player.GetComponent<Health>();

            if (!Platform.IsMobile)
            {
                this._mobileJoystick.SetActive(false);
            }
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && !this._playerHealth.IsDead)
            {
                if (this._gamePausedScreenInstance != null)
                {
                    this.ResumeGame();
                }
                else
                {
                    this.TogglePausedState();

                    this._gamePausedScreenInstance = this.SetInGameMenuEvents(this.ResumeGame, "ResumeButton", this._gamePausedScreen);
                }
            }
        }

        /// <summary>
        /// Called when player dies
        /// </summary>
        public void OnPlayerDeath()
        {
            if (this._gameOverScreen == null) throw new InvalidOperationException("No game end screen prefab found!");

            this.SetInGameMenuEvents(this.RestartGame, "RestartButton", this._gameOverScreen);
        }

        private void ToggleSound()
        {
            if (!this._gameIsPaused || this._gamePausedScreenInstance == null) return;

            var isCurrentlyMuted = AudioListener.volume == 0f;

            AudioListener.volume = isCurrentlyMuted ? 1f : 0f;

            var btn = this._gamePausedScreenInstance.transform.GetChild(0).Find("SoundToggleButton");
            var btnText = btn?.GetComponentInChildren<Text>();

            if (btn == null || btnText == null) return;

            // TODO [GM]: fix muted text when menu is opened from in game
            btnText.text = $"Sound " + (isCurrentlyMuted ? "On" : "Off");
        }

        private void RestartGame()
        {
            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }

        private void ResumeGame()
        {
            this.TogglePausedState();

            if (this._gameIsPaused || this._gamePausedScreenInstance == null) return;

            GameObject.Destroy(this._gamePausedScreenInstance);
            this._gamePausedScreenInstance = null;
        }

        private void TogglePausedState()
        {
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
            this._gameIsPaused = this._player.TogglePausedState();
        }

        private void QuitGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
        }

        private GameObject SetInGameMenuEvents(UnityAction mainBtnAction, string mainBtnName, GameObject prefab)
        {
            var prefabInstance = GameObject.Instantiate(prefab);
            prefabInstance.GetComponentInChildren<Canvas>().worldCamera = Camera.main;

            var hud = prefabInstance.transform.GetChild(0);

            var btn = hud.transform.Find(mainBtnName).GetComponentInChildren<Button>();
            btn?.onClick.AddListener(mainBtnAction);

            var quitBtn = hud.transform.Find("QuitButton")?.GetComponent<Button>();
            quitBtn?.onClick.AddListener(this.QuitGame);

            var soundBtn = hud.transform.Find("SoundToggleButton")?.GetComponent<Button>();

            if (soundBtn)
            {
                var soundBtnText = soundBtn.GetComponentInChildren<Text>();
                var isSoundMuted = AudioListener.volume <= 0f;
                soundBtnText.text = $"Sound " + (isSoundMuted ? "Off" : "On");

                soundBtn.onClick.AddListener(this.ToggleSound);
            }

            return prefabInstance;
        }
    }
}