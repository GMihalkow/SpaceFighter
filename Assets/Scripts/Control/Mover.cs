using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;

        private MapBounds _mapBounds;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        public void Move(Vector3 addition)
        {
            var currPos = this.transform.position;
            var x = Mathf.Clamp((addition.x * this._speed * Time.deltaTime) + currPos.x, this._mapBounds.MinX, this._mapBounds.MaxX);
            var y = Mathf.Clamp((addition.y * this._speed * Time.deltaTime) + currPos.y, this._mapBounds.MinY, this._mapBounds.MaxY);

            this.transform.position = new Vector3(x, y);
        }
    }
}