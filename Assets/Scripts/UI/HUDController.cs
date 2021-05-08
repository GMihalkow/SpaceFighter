using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] GameObject _gameOverScreen;

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
    }
}