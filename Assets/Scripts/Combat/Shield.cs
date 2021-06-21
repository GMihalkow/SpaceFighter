﻿using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] GameObject _hitEffect;
        [SerializeField] float _healthDecreaseOnHit = 5f;

        private GameObject _target;
        private Health _health;

        private void Awake()
        {
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
            if (projectile == null) return;

            GameObject.Instantiate(this._hitEffect, collision.transform.position, Quaternion.identity);
        }
    }
}