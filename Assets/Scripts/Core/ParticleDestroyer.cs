using UnityEngine;

namespace SpaceFighter.Core
{
    public class ParticleDestroyer : MonoBehaviour
    {
        private void Start()
        {
            var ps = this.GetComponent<ParticleSystem>();
            if (ps == null) return;

            GameObject.Destroy(this.gameObject, ps.duration);
        }
    }
}