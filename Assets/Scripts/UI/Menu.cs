using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceFighter.UI
{
    public class Menu : MonoBehaviour
    {
        /// <summary>
        /// Called from canvas button
        /// </summary>
        public void OnPlay()
        {
            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }
    }
}