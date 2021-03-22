using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] float _rotationSpeed = 20f;
        [SerializeField] float _flightSpeed = 1f;

        private float _bottomOffset = 1f;
        private SpriteRenderer _spriteRenderer;
        private MapBounds _mapBounds;

        private void Awake()
        {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private void Update()
        {
            this.transform.position += (Vector3.down * (this._flightSpeed * Time.deltaTime));
            this.transform.Rotate(Vector3.back * (this._rotationSpeed * Time.deltaTime));

            if (this._mapBounds.IsBelowBounds(this.transform.position.y + this._bottomOffset + (this._spriteRenderer.sprite.bounds.size.y / 2)))
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}