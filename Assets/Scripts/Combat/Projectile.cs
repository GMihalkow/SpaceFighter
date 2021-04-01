using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        private float _speed;
        private float _attackDamage;
        private MapBounds _mapBounds;
        private GameObject _hitEffectPrefab;

        public float AttackDamage => this._attackDamage;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        public void SetConfig(float speed, float attackDamage, GameObject hitEffectPrefab)
        {
            this._speed = speed;
            this._attackDamage = attackDamage;
            this._hitEffectPrefab = hitEffectPrefab;
        }

        public void PlayHitEffect()
        {
            GameObject.Instantiate(this._hitEffectPrefab, this.transform.position, Quaternion.identity);
        }

        private void Update()
        {
            this.transform.Translate(Vector3.right * (this._speed * Time.deltaTime), Space.Self);
            
            if (!this._mapBounds.IsInBounds(this.transform.position))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}