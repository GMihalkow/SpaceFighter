using UnityEngine;

namespace SpaceFighter.Core
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleDestroyer : MonoBehaviour
    {
        private ParticleSystem _ps;

        private void Awake()
        {
            this._ps = this.GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (this._ps.isStopped) GameObject.Destroy(this.gameObject);
        }
    }
}