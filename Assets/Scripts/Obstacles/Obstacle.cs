using SpaceFighter.Combat;
using SpaceFighter.Core;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHit;
        [SerializeField] float _staticDamage = 5f;
        [SerializeField] float _minSpeed = 1f;
        [SerializeField] float _maxSpeed = 3f;
        
        protected float _speed;
        private Health _health;

        public float StaticDamage => this._staticDamage;

        protected virtual void Awake()
        {
            this._health = this.GetComponent<Health>();
            this._speed = Random.Range(this._minSpeed, this._maxSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            var shield = collision.GetComponent<Shield>();
            
            if (this._health.IsDead || (shield == null && projectile == null) || this.CompareTag(collision.tag) || projectile?.HasExploded == true) return;

            this._onHit?.Invoke();

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