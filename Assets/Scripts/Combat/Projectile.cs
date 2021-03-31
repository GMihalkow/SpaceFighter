using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        private bool _positiveDirection;
        private float _speed;
        private float _attackDamage;
        private MapBounds _mapBounds;
        private GameObject _hitEffectPrefab;

        public float AttackDamage => this._attackDamage;

        public void SetConfig(bool positiveDirection, float speed, float attackDamage, GameObject hitEffectPrefab)
        {
            this._speed = speed;
            this._attackDamage = attackDamage;
            this._hitEffectPrefab = hitEffectPrefab;
            this._positiveDirection = positiveDirection;
        }

        public void PlayHitEffect()
        {
            GameObject.Instantiate(this._hitEffectPrefab, this.transform.position, Quaternion.identity);
        }

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private void Update()
        {
            if (this._positiveDirection)
            {
                this.transform.position += Vector3.up * (this._speed * Time.deltaTime);
            }
            else
            {
                this.transform.position += Vector3.down * (this._speed * Time.deltaTime);
            }

            if (!this._mapBounds.IsInBounds(this.transform.position))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}