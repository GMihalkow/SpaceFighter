using UnityEngine;

namespace SpaceFighter.Core
{
    public class AnimationDestroyer : MonoBehaviour
    {
        /// <summary>
        /// Animation event
        /// </summary>
        private void OnEnd()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}