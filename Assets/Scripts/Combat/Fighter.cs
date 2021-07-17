using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] ProjectileSpawner _projectileSpawner;
        [SerializeField] float _attackDamage = 5f;
        [SerializeField] GameObject _projectilesContainer;
        [SerializeField] UnityEvent _onShoot;

        private void Awake()
        {
            this._projectileSpawner.Container = this._projectilesContainer;
        }

        private void Start()
        {
            this.StartCoroutine(this._projectileSpawner.Prepare());
        }

        public void Shoot()
        {
            this._onShoot?.Invoke();
            this._projectileSpawner.Shoot(this.transform.position, this._attackDamage, this.transform.rotation);
        }
    }
}