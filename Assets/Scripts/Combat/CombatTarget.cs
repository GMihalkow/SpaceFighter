using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        // TODO [GM]: Extract and share logic with ObstacleTarget?
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();
            if (projectile == null || projectile.CompareTag(this.tag)) return;

            projectile.PlayHitEffect();
            GameObject.Destroy(projectile.gameObject);

            this._health.TakeDamage(projectile.AttackDamage);
        }
    }
}