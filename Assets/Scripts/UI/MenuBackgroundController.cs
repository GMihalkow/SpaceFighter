using UnityEngine;

namespace SpaceFighter.UI
{
    public class MenuBackgroundController : MonoBehaviour
    {
        [SerializeField] GameObject _bgMoverPrefab;

        private void Start()
        {
            for (int index = 0; index < this.transform.childCount; index++)
            {
                var bg = this.transform.GetChild(index);
                bg.GetComponent<MenuBackground>().OnLeave += this.OnLeave;
            }
        }
        
        private void OnLeave(object sender, Vector3 pos)
        {
            var instance = GameObject.Instantiate(this._bgMoverPrefab, pos, Quaternion.identity, this.transform);
            instance.GetComponent<MenuBackground>().OnLeave += this.OnLeave;
        }
    }
}