using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] GameObject _explosionPrefab;
        [SerializeField] float _healthPoints = 100f;
        [SerializeField] UnityEvent _onDeath;
        [SerializeField] OnHealthChangeEvent _onHealthChange;

        [Serializable]
        public class OnHealthChangeEvent : UnityEvent<float> { }

        private bool _isDead;
        private float _initialHealthPoints;

        public UnityEvent OnDeath => this._onDeath;

        public float InitialHealthPoints => this._initialHealthPoints;

        public float HealthPoints => this._healthPoints;

        public bool IsDead => this._isDead;

        private void Awake()
        {
            this._initialHealthPoints = this._healthPoints;
        }

        public void TakeDamage(float damage)
        {
            if (this._isDead) return;

            this._healthPoints = Mathf.Max(0, this._healthPoints - damage);
            this._isDead = this._healthPoints <= 0;

            this._onHealthChange?.Invoke(this._healthPoints / this._initialHealthPoints);

            if (this._healthPoints <= 0) this._onDeath.Invoke();
        }

        public void ResetHealth()
        {
            this._healthPoints = this._initialHealthPoints;
            this._onHealthChange.Invoke(1f);
        }

        public void Explode(bool destroyAfter = true)
        {
            GameObject.Instantiate(this._explosionPrefab, this.transform.position, Quaternion.identity);
            
            if (destroyAfter) GameObject.Destroy(this.gameObject);
        }
    }
}