using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _healthPoints = 100f;
        [SerializeField] UnityEvent _onDeath;

        private bool _isDead;

        public bool IsDead => this._isDead;

        public void TakeDamage(float damage)
        {
            this._isDead = this._healthPoints <= 0;

            if (this._isDead) return;

            this._healthPoints = Mathf.Max(0, this._healthPoints - damage);

            if (this._healthPoints <= 0) this._onDeath.Invoke();
        }
    }
}