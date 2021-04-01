using UnityEngine;

namespace SpaceFighter.Combat
{
    [CreateAssetMenu(fileName = "ProjectileSpawner", menuName = "Projectiles/Create Spawner", order = 0)]
    public class ProjectileSpawner : ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _hitEffectPrefab;
        [SerializeField] float _speed = 5;

        public void Shoot(Vector2 startPos, float attackDamage, Quaternion rotation)
        {
            var projectile = GameObject.Instantiate(this._prefab, startPos, rotation);
            projectile.GetComponent<Projectile>().SetConfig(this._speed, attackDamage, this._hitEffectPrefab);
        }
    }
}