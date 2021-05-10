using SpaceFighter.AI;
using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Obstacles
{
    [RequireComponent(typeof(Health))]
    public class ObstacleTarget : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obstacleComponent = collision.gameObject.GetComponent<Obstacle>();

            if (obstacleComponent == null) return;

            this._health.TakeDamage(obstacleComponent.StaticDamage);
            var isEnemy = obstacleComponent.CompareTag("Enemy");
            
            if (isEnemy)
            {
                obstacleComponent.GetComponent<BaseAI>().OnDeath();
            }
            else
            {
                obstacleComponent.GetComponent<Health>().Explode();
            }
        }
    }
}