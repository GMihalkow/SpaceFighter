using UnityEngine;

namespace SpaceFighter.Core
{
    public class MapBounds : MonoBehaviour
    {
        [SerializeField] float _playerMapBoundsFragment = 0.8f;

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
        private float _playerMinY;
        private float _playerMaxY;

        public float MinX => this._minX;

        public float MaxX => this._maxX;

        public float MinY => this._minY;

        public float MaxY => this._maxY;

        public float PlayerMinY => this._playerMinY;
        
        public float PlayerMaxY => this._playerMaxY;

        private void Awake()
        {
            this._mainCamera = Camera.main;
            this._bg = GameObject.FindGameObjectWithTag("LevelBackground").GetComponent<SpriteRenderer>();

            this._maxY = this._bg.bounds.size.x / 2;
            this._minY = this._maxY * -1;
            this._maxX = this._bg.bounds.size.y / 2;
            this._minX = this._maxX * -1;

            this._playerMinY = this.MinY * this._playerMapBoundsFragment;
            this._playerMaxY = this.MaxY * this._playerMapBoundsFragment;
        }

        private void LateUpdate()
        {
            this._maxCameraY = this.transform.position.y + (this._mainCamera.orthographicSize / 2);
            this._minCameraY = this.transform.position.y - (this._mainCamera.orthographicSize / 2);
            this._maxCameraX = this.transform.position.x + (this._mainCamera.orthographicSize * this._mainCamera.aspect);
            this._minCameraX = this.transform.position.x - (this._mainCamera.orthographicSize * this._mainCamera.aspect);
        }

        /// <summary>
        /// Generates a random position at the left of the camera view
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Vector2 GeneratePositionLeftOfCamera(float offset = 0f)
        {
            var y = Random.Range(Mathf.Max(this._minCameraY, this.MinY), Mathf.Min(this._maxCameraY, this.MaxY));

            return new Vector2(this._minCameraX - offset, y);
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