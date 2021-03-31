using SpaceFighter.Combat;
using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] GameObject _explosionEffectPrefab;
        [SerializeField] float _staticDamage = 5f;
        
        private Health _health;

        public float StaticDamage => this._staticDamage;

        protected virtual void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        public void Explode()
        {
            GameObject.Instantiate(this._explosionEffectPrefab, this.transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            if (this._health.IsDead || projectile == null) return;

            GameObject.Destroy(projectile.gameObject);

            this._health.TakeDamage(projectile.AttackDamage);
        }
    }
}