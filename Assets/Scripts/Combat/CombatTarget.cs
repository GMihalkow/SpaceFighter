using SpaceFighter.Core;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHit;

        private Health _health;

        private void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        // TODO [GM]: Extract and share logic with ObstacleTarget?
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            if (projectile == null || projectile.CompareTag(this.tag) || projectile.HasExploded) return;

            projectile.PlayHitEffect(true);

            this._onHit?.Invoke();
            this._health.TakeDamage(projectile.AttackDamage);
        }
    }
}