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
        [SerializeField] float _hitSlowDownTimeout = 2f;
        
        protected float _speed;
        private float _initialSpeed;
        private bool _isHit;
        private float _timeSinceSlowDown;
        private Health _health;

        public float StaticDamage => this._staticDamage;

        protected virtual void Awake()
        {
            this._health = this.GetComponent<Health>();
            this._speed = Random.Range(this._minSpeed, this._maxSpeed);
            this._initialSpeed = this._speed;
        }

        protected virtual void Update()
        {
            if (!this._isHit) return;

            if (Mathf.Approximately(this._timeSinceSlowDown, this._hitSlowDownTimeout) || this._timeSinceSlowDown > this._hitSlowDownTimeout)
            {
                this._isHit = false;
                this._timeSinceSlowDown = 0f;
                this._speed = this._initialSpeed;
            }

            this._timeSinceSlowDown += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            var shield = collision.GetComponent<Shield>();
            
            if (this._health.IsDead || (shield == null && projectile == null) || this.CompareTag(collision.tag) || projectile?.HasExploded == true) return;

            this._onHit?.Invoke();

            this._isHit = true;
            this._speed = this._minSpeed;
            this._timeSinceSlowDown = 0f;

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