using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceFighter.Combat
{
    [CreateAssetMenu(fileName = "ProjectileSpawner", menuName = "Projectiles/Create Spawner", order = 0)]
    public class ProjectileSpawner : ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _hitEffectPrefab;
        [SerializeField] float _speed = 5;
        [SerializeField] float _destroyTimeout = 3f;

        private GameObject _container;
        private int _currentProjectileIndex = 0;

        public GameObject Container { set => this._container = value; }

        public void Prepare(int projectilesCount)
        {
            for (int index = 0; index < projectilesCount; index++)
            {
                GameObject.Instantiate(this._prefab, this._container.transform.position, this._container.transform.rotation, this._container.transform);
            }
        }

        public void Shoot(Vector2 startPos, float attackDamage, Quaternion rotation)
        {
            if (this._container == null) return;
            if (this._currentProjectileIndex >= this._container.transform.childCount) this._currentProjectileIndex = 0;

            var projectile = this._container.transform.GetChild(this._currentProjectileIndex)?.GetComponent<Projectile>();
            this._currentProjectileIndex++;

            projectile.SetConfig(this._speed, attackDamage, this._hitEffectPrefab, this._destroyTimeout);
            projectile.Activate(startPos, rotation);
        }
    }
}