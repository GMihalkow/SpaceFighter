using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceFighter.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] float _playBtnDelay = 0.05f;

        private bool _btnIsClicked;

        /// <summary>
        /// Called from canvas button
        /// </summary>
        public void OnPlay()
        {
            if (this._btnIsClicked) return;

            this._btnIsClicked = true;

            this.StartCoroutine(this.OnPlayCoroutine());
        }

        IEnumerator OnPlayCoroutine()
        {
            yield return new WaitForSecondsRealtime(this._playBtnDelay);

            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }
    }
}