using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Movement
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

        /// <summary>
        /// Rotates sprite in the Z to point towards specific point. 
        /// Make sure your sprite is pointing towards the positive X .
        /// </summary>
        /// <param name="screenCoords">in screen point units</param>
        public void LookAt(Vector3 screenCoords)
        {
            var dir = screenCoords - Camera.main.WorldToScreenPoint(this.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}