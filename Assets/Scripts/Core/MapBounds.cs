using UnityEngine;

namespace SpaceFighter.Core
{
    public class MapBounds : MonoBehaviour
    {
        private SpriteRenderer _bg;
        private Camera _mainCamera;
        private float _minY;
        private float _maxY;
        private float _minX;
        private float _maxX;
        private float _minCameraY;
        private float _maxCameraY;
        private float _minCameraX;
        private float _maxCameraX;

        public float MinX => this._minX;

        public float MaxX => this._maxX;

        public float MinY => this._minY;

        public float MaxY => this._maxY;

        private void Awake()
        {
            this._mainCamera = Camera.main;
            this._bg = GameObject.FindGameObjectWithTag("LevelBackground").GetComponent<SpriteRenderer>();

            this._maxY = this._bg.bounds.size.x / 2;
            this._minY = this._maxY * -1;
            this._maxX = this._bg.bounds.size.y / 2;
            this._minX = this._maxX * -1;
        }

        private void LateUpdate()
        {
            this._maxCameraY = this.transform.position.y + (this._mainCamera.orthographicSize / 2);
            this._minCameraY = this.transform.position.y - (this._mainCamera.orthographicSize / 2);
            this._maxCameraX = this.transform.position.x + (this._mainCamera.orthographicSize * this._mainCamera.aspect);
            this._minCameraX = this.transform.position.x - (this._mainCamera.orthographicSize * this._mainCamera.aspect);
        }

        /// <summary>
        /// Generates a random position at the top of the camera view
        /// </summary>
        /// <param name="topOffset"></param>
        /// <returns></returns>
        public Vector2 GeneratePositionAboveCamera(float topOffset = 0f)
        {
            var x = Random.Range(Mathf.Max(this._minCameraX, this.MinX), Mathf.Min(this._maxCameraX, this.MaxX));

            return new Vector2(x, this._maxCameraY + topOffset);
        }

        public bool IsInBounds(Vector2 pos, float offset = 0) => (pos.x >= this._minX - offset && pos.x <= this._maxX + offset) && (pos.y >= this._minY - offset && pos.y <= this._maxY + offset);

        public bool IsBelowBounds(float y) => y <= this.MinY;
    }
}