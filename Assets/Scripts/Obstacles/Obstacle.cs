using SpaceFighter.Combat;
using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] float _staticDamage = 5f;
        
        private Health _health;

        public float StaticDamage => this._staticDamage;

        protected virtual void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            if (this._health.IsDead || projectile == null || this.CompareTag(this.tag)) return;

            projectile.PlayHitEffect();
            GameObject.Destroy(projectile.gameObject);

            this._health.TakeDamage(projectile.AttackDamage);
        }
    }
}