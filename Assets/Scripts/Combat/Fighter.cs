using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] ProjectileSpawner _projectileSpawner;
        [SerializeField] float _attackDamage = 5f;
        [SerializeField] UnityEvent _onShoot;

        public void Shoot()
        {
            this._onShoot?.Invoke();
            this._projectileSpawner.Shoot(this.transform.position, this._attackDamage, this.transform.rotation);
        }
    }
}