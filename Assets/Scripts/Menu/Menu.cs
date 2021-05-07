using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceFighter.Menu
{
    public class Menu : MonoBehaviour
    {
        public void OnPlay()
        {
            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }
    }
}