using SpaceFighter.Core;
using SpaceFighter.Movement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHitSound;
        [SerializeField] float _maxLife = 2f;
        [SerializeField] float _destroyOffset = 5f;

        protected Mover _mover;
        protected bool _hasExploded;
        private float _speed;
        private float _attackDamage;
        private MapBounds _mapBounds;
        private SpriteRenderer _spriteRenderer;
        private GameObject _hitEffectPrefab;

        public event Action OnExplode;

        public bool HasExploded => this._hasExploded;

        public float AttackDamage => this._attackDamage;

        protected virtual void Awake()
        {
            this.gameObject.SetActive(false);
            this._mover = this.GetComponent<Mover>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        }

        protected virtual void FixedUpdate()
        {
            if (this._hasExploded || !this.gameObject.activeSelf) return;

            this._mover.Move(Vector3.right * this._speed, Space.Self, false);

            if (!this._mapBounds.IsInBounds(this.transform.position, this._destroyOffset))
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public void SetConfig(float speed, float attackDamage, GameObject hitEffectPrefab)
        {
            this._speed = speed;
            this._attackDamage = attackDamage;
            this._hitEffectPrefab = hitEffectPrefab;
        }

        public void PlayHitEffect(bool playSound)
        {
            if (this._hasExploded || !this.gameObject.activeSelf) return;

            this._hasExploded = true;
            this._spriteRenderer.enabled = false;

            if (playSound)
            {
                this._onHitSound?.Invoke();
            }

            GameObject.Instantiate(this._hitEffectPrefab, this.transform.position, Quaternion.identity);
            this.OnExplode?.Invoke();
            this.gameObject.SetActive(false);
        }

        public void Activate(Vector2 startPos, Quaternion rotation)
        {
            this.StopAllCoroutines();

            this._hasExploded = false;
            this._spriteRenderer.enabled = true;
            this.gameObject.transform.rotation = rotation;
            this.gameObject.transform.position = new Vector3(startPos.x, startPos.y);
            this.gameObject.SetActive(true);

            this.StartCoroutine(this.OnActivateCoroutine());
        }

        private IEnumerator OnActivateCoroutine()
        {
            yield return new WaitForSeconds(this._maxLife);

            this.PlayHitEffect(false);
        }
    }
}