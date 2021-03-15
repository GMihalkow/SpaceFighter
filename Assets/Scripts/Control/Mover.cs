using UnityEngine;

namespace SpaceFighter.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;

        private Camera _mainCamera;
        private float _minY;
        private float _maxY;
        private float _minX;
        private float _maxX;

        private void Awake()
        {
            this._mainCamera = Camera.main;

            this._maxY = this._mainCamera.orthographicSize;
            this._minY = this._maxY * -1;
            this._maxX = this._mainCamera.orthographicSize * this._mainCamera.aspect;
            this._minX = this._maxX * -1;
        }

        public void Move(Vector3 addition)
        {
            var currPos = this.transform.position;
            var x = Mathf.Clamp((addition.x * this._speed * Time.deltaTime) + currPos.x, this._minX, this._maxX);
            var y = Mathf.Clamp((addition.y * this._speed * Time.deltaTime) + currPos.y, this._minY, this._maxY);

            this.transform.position = new Vector3(x, y);
        }
    }
}