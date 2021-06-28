using SpaceFighter.Core;
using SpaceFighter.Effects;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] GameObject _hitEffect;
        [SerializeField] float _healthDecreaseOnHit = 5f;

        private GameObject _target;
        private SpriteColorFader _spriteFader;
        private Health _health;

        public bool HealthIsFull => this._health.InitialHealthPoints == this._health.HealthPoints;

        private void Awake()
        {
            this._spriteFader = this.GetComponent<SpriteColorFader>();
            this._health = this.GetComponent<Health>();
        }

        private void LateUpdate()
        {
            if (this._target == null) return;

            this.transform.position = this._target.transform.position;
        }

        public void DecreaseHealth()
        {
            this._health.TakeDamage(this._healthDecreaseOnHit);
        }

        public void SetTarget(GameObject target)
        {
            this._target = target;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            
            if (projectile == null || projectile.HasExploded)
            {
                if (collision.CompareTag(this.tag) || collision.CompareTag("ShieldRestore")) return;
                
                this._spriteFader.Fade();
                
                return;
            }

            GameObject.Instantiate(this._hitEffect, collision.transform.position, Quaternion.identity);
        }
    }
}