using UnityEngine;

namespace SpaceFighter.Core
{
    public class MapBounds : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _minY;
        private float _maxY;
        private float _minX;
        private float _maxX;

        public float MinX => this._minX;

        public float MaxX => this._maxX;

        public float MinY => this._minY;

        public float MaxY => this._maxY;

        private void Awake()
        {
            this._mainCamera = Camera.main;

            this._maxY = this._mainCamera.orthographicSize;
            this._minY = this._maxY * -1;
            this._maxX = this._mainCamera.orthographicSize * this._mainCamera.aspect;
            this._minX = this._maxX * -1;
        }

        public bool IsInBounds(Vector2 pos) => (pos.x >= this._minX && pos.x <= this._maxX) && (pos.y >= this._minY && pos.y <= this._maxY);

        public bool IsBelowBounds(float y) => y <= this.MinY;
    }
}