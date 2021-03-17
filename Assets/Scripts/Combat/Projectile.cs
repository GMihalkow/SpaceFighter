using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] bool _positiveDirection = false;
        [SerializeField] float _speed = 5;

        private MapBounds _mapBounds;

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
                return;
            }
        }
    }
}