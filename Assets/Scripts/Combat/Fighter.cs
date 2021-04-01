using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] ProjectileSpawner _projectileSpawner;
        [SerializeField] float _attackDamage = 5f;

        public void Shoot()
        {
            this._projectileSpawner.Shoot(this.transform.position, this._attackDamage, this.transform.rotation);
        }
    }
}