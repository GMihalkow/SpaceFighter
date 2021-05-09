using SpaceFighter.Control;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] GameObject _gameOverScreen;
        [SerializeField] GameObject _gamePausedScreen;

        private GameObject _gamePausedScreenInstance;
        private bool _gameIsPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (this._gamePausedScreenInstance != null)
                {
                    this.ResumeGame();
                }
                else
                {
                    this.TogglePausedState();

                    this._gamePausedScreenInstance = GameObject.Instantiate(this._gamePausedScreen);
                    this._gamePausedScreenInstance.GetComponentInChildren<Canvas>().worldCamera = Camera.main;

                    var btn = this._gamePausedScreenInstance.GetComponentInChildren<Button>();
                    btn?.onClick.AddListener(this.ResumeGame);
                }
            }
        }

        /// <summary>
        /// Called when player dies
        /// </summary>
        public void OnPlayerDeath()
        {
            if (this._gameOverScreen == null) throw new InvalidOperationException("No game end screen prefab found!");

            var gameOverScreen = GameObject.Instantiate(this._gameOverScreen);
            gameOverScreen.GetComponentInChildren<Canvas>().worldCamera = Camera.main;

            var btn = gameOverScreen.GetComponentInChildren<Button>();
            btn?.onClick.AddListener(this.RestartGame);
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
    }
}