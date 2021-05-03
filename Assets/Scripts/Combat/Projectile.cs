using SpaceFighter.Core;
using System.Collections;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _maxLife = 2f;
        [SerializeField] float _destroyOffset = 5f;

        private float _speed;
        private float _attackDamage;
        private MapBounds _mapBounds;
        private GameObject _hitEffectPrefab;

        public float AttackDamage => this._attackDamage;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(this._maxLife);

            this.PlayHitEffect();
            
            GameObject.Destroy(this.gameObject);
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
            
            if (!this._mapBounds.IsInBounds(this.transform.position, this._destroyOffset))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}