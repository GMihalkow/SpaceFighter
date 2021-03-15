using UnityEngine;

namespace SpaceFighter.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;

        private Camera _mainCamera;
        private SpriteRenderer _spriteRenderer;
        private float _minY;
        private float _maxY;
        private float _minX;
        private float _maxX;

        private void Awake()
        {
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._mainCamera = Camera.main;

            this._maxY = this._mainCamera.orthographicSize;
            this._minY = this._maxY * -1;
            this._maxX = this._mainCamera.orthographicSize * this._mainCamera.aspect;
            this._minX = this._maxX * -1;
        }

        private void Update()
        {
            // TODO [GM]: Extract input handling to a PlayerController?
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            var currPos = this.transform.position;
            var x = Mathf.Clamp((xAxis * this._speed * Time.deltaTime) + currPos.x, this._minX, this._maxX);
            var y = Mathf.Clamp((yAxis * this._speed * Time.deltaTime) + currPos.y, this._minY, this._maxY);
            var futurePosition = new Vector3(x, y);

            this.transform.position = futurePosition;
        }
    }
}