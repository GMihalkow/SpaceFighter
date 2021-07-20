using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter.Combat
{
    [CreateAssetMenu(fileName = "ProjectileSpawner", menuName = "Projectiles/Create Spawner", order = 0)]
    public class ProjectileSpawner : ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _hitEffectPrefab;
        [SerializeField] float _speed = 5;

        private GameObject _container;
        private Queue<GameObject> _projectiles;

        public GameObject Container { set => this._container = value; }

        public IEnumerator Prepare(int projectilesCount)
        {
            this._projectiles = new Queue<GameObject>(projectilesCount);

            for (int index = 0; index < projectilesCount; index++)
            {
                if (index % 2 == 0) yield return new WaitForEndOfFrame();

                var instance = GameObject.Instantiate(this._prefab, this._container.transform.position, this._container.transform.rotation, this._container.transform);
                this._projectiles.Enqueue(instance);

                instance.GetComponent<Projectile>().OnExplode += () => this._projectiles.Enqueue(instance);
            }
        }

        public void Shoot(Vector2 startPos, float attackDamage, Quaternion rotation)
        {
            var projectile = this._projectiles.Dequeue().GetComponent<Projectile>();

            projectile.SetConfig(this._speed, attackDamage, this._hitEffectPrefab);
            projectile.Activate(startPos, rotation);
        }
    }
}