using UnityEngine;

namespace SpaceFighter.Combat
{
    [CreateAssetMenu(fileName = "ProjectileSpawner", menuName = "Projectiles/Create Spawner", order = 0)]
    public class ProjectileSpawner : ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] float _speed = 5;
        [SerializeField] bool _positiveDirection = false;

        public void Shoot(Vector2 startPos, float attackDamage)
        {
            var projectile = GameObject.Instantiate(this._prefab, startPos, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetConfig(this._positiveDirection, this._speed, attackDamage);
        }
    }
}