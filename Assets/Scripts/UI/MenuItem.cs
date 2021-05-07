using SpaceFighter.Core;
using SpaceFighter.Movement;
using UnityEngine;

namespace SpaceFighter.UI
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] float _destroyOffset = 1f;

        private SpriteRenderer _sprite;
        private Mover _mover;
        private MapBounds _mapBounds;

        private void Awake()
        {
            this._sprite = this.GetComponent<SpriteRenderer>();
            this._mover = this.GetComponent<Mover>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private void Update()
        {
            this._mover.Move(Vector3.right, removeIfBelowMap: false);

            var width = this._sprite.bounds.size.x;

            if (this.transform.position.x - (width / 2) - this._destroyOffset >= this._mapBounds.MaxX)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}