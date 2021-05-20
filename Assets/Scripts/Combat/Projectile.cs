using SpaceFighter.Core;
using SpaceFighter.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHitSound;
        [SerializeField] float _explosionTimeout = 2f;
        [SerializeField] float _maxLife = 2f;
        [SerializeField] float _destroyOffset = 5f;

        protected Mover _mover;
        private float _speed;
        private float _attackDamage;
        private MapBounds _mapBounds;
        private SpriteRenderer _spriteRenderer;
        private GameObject _hitEffectPrefab;
        private bool _hasExploded;

        public bool HasExploded => this._hasExploded;

        public float AttackDamage => this._attackDamage;

        protected virtual void Awake()
        {
            this._mover = this.GetComponent<Mover>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this._maxLife);

            this.PlayHitEffect(false);
        }

        public void SetConfig(float speed, float attackDamage, GameObject hitEffectPrefab)
        {
            this._speed = speed;
            this._attackDamage = attackDamage;
            this._hitEffectPrefab = hitEffectPrefab;
        }

        public void PlayHitEffect(bool playSound, bool useDestroyTimeout = false)
        {
            if (this._hasExploded) return;

            this._hasExploded = true;
            GameObject.Destroy(this._spriteRenderer);

            if (playSound)
            {
                this._onHitSound?.Invoke();
            }

            GameObject.Instantiate(this._hitEffectPrefab, this.transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject, useDestroyTimeout ? this._explosionTimeout : 0);
        }

        protected virtual void Update()
        {
            if (this._hasExploded) return;

            this._mover.Move(Vector3.right * this._speed, Space.Self, false);

            if (!this._mapBounds.IsInBounds(this.transform.position, this._destroyOffset))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}