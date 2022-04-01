using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] float _playBtnDelay = 0.05f;
        [SerializeField] GameObject _soundBtn;

        private bool _btnIsClicked;

        private void Start()
        {
            var isSoundMuted = AudioListener.volume <= 0f;
            this._soundBtn.GetComponentInChildren<Text>().text = $"Sound " + (isSoundMuted ? "Off" : "On"); ;
        }

        /// <summary>
        /// Called from canvas button
        /// </summary>
        public void OnPlay()
        {
            if (this._btnIsClicked) return;

            this._btnIsClicked = true;

            this.StartCoroutine(this.OnPlayCoroutine());
        }

        /// <summary>
        /// Called from canvas button
        /// </summary>
        public void OnSoundToggle(Text btnText)
        {
            var isCurrentlyMuted = AudioListener.volume == 0f;

            AudioListener.volume = isCurrentlyMuted ? 1f : 0f;

            btnText.text = $"Sound " + (isCurrentlyMuted ? "On" : "Off");
        }

        public void OnExit() => Application.Quit();

        IEnumerator OnPlayCoroutine()
        {
            yield return new WaitForSecondsRealtime(this._playBtnDelay);

            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }
    }
}