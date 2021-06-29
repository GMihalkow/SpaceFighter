using SpaceFighter.Core;
using SpaceFighter.Movement;
using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public class Asteroid : Obstacle
    {
        [SerializeField] float _rotationSpeed = 20f;

        private float _bottomOffset = 1f;
        private SpriteRenderer _spriteRenderer;
        private MapBounds _mapBounds;
        private Mover _mover;

        protected override void Awake()
        {
            base.Awake();

            this._mover = this.GetComponent<Mover>();
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        protected override void Update()
        {
            base.Update();

            this._mover.Move(Vector3.down * this._speed);
            this.transform.Rotate(Vector3.back * (this._rotationSpeed * Time.deltaTime));
        }

        private void LateUpdate()
        {
            if (!this._mapBounds.IsBelowBounds(this.transform.position.y + this._bottomOffset + (this._spriteRenderer.sprite.bounds.size.y / 2))) return;

            GameObject.Destroy(this.gameObject);
        }
    }
}