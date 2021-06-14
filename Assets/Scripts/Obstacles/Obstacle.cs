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
            var shield = collision.GetComponent<Shield>();
            
            if (this._health.IsDead || (shield == null && projectile == null) || this.CompareTag(collision.tag) || projectile?.HasExploded == true) return;

            if (projectile != null)
            {
                projectile.PlayHitEffect(true, true);
                this._health.TakeDamage(projectile.AttackDamage);
            }
            else
            {
                shield.DecreaseHealth();
                this._health.Explode();
            }
        }
    }
}