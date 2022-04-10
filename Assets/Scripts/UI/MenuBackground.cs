using SpaceFighter.Core;
using SpaceFighter.Movement;
using System;
using UnityEngine;

namespace SpaceFighter.UI
{
    public class MenuBackground : MonoBehaviour
    {
        private MapBounds _mapBounds;
        private Mover _mover;
        private SpriteRenderer _leftTile;
        private bool _hasPassed;

        public event EventHandler<Vector3> OnLeave;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._leftTile = this.transform.Find("LeftBg").GetComponent<SpriteRenderer>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            var edgePos = this._leftTile.transform.position;
            edgePos.x -= (this._leftTile.bounds.size.x / 2);

            if (edgePos.x >= this._mapBounds.MaxX)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            this._mover.Move(Vector3.right);
        }

        private void LateUpdate()
        {
            if (this._leftTile.transform.position.x < this._mapBounds.MinX || this._hasPassed) return;

            var edgePos = this._leftTile.transform.position;
            edgePos.x -= (this._leftTile.bounds.size.x / 2);
            edgePos.x -= this._leftTile.bounds.size.x - (this._leftTile.bounds.size.x / 2);

            this._hasPassed = true;

            this.OnLeave.Invoke(this, edgePos);
        }
    }
}