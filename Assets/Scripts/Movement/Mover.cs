using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _maxSpeed = 10f;
        [SerializeField] float _minSpeed = 3f;

        private SpriteRenderer _spriteRenderer;
        private MapBounds _mapBounds;
        private bool _useMinSpeed;

        public bool UseMinSpeed
        { 
            set
            {
                this._useMinSpeed = value;
            } 
        }

        private void Awake()
        {
            this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        /// <summary>
        /// Moves object within map bounds and doesn't let it pass them
        /// </summary>
        /// <param name="addition">amount to move object with</param>
        /// <param name="space">world of local axis</param>
        public void MoveInBounds(Vector3 addition, Space space = Space.World, bool isPlayer = false)
        {
            var speed = this._useMinSpeed ? this._minSpeed : this._maxSpeed;
            var minY = isPlayer ? this._mapBounds.PlayerMinY : this._mapBounds.MinY;
            var maxY = isPlayer ? this._mapBounds.PlayerMaxY : this._mapBounds.MaxY;

            var currPos = this.transform.position;
            var x = Mathf.Clamp((addition.x * speed * Time.deltaTime) + currPos.x, this._mapBounds.MinX, this._mapBounds.MaxX);
            var y = Mathf.Clamp((addition.y * speed * Time.deltaTime) + currPos.y, minY, maxY);

            if (space == Space.World)
            {
                this.transform.position = new Vector3(x, y);
            }
            else
            {
                var diff = new Vector3(x, y, this.transform.position.z) - this.transform.position;

                this.transform.Translate(diff, space);
            }
        }

        /// <summary>
        /// Moves object in world
        /// </summary>
        /// <param name="addition">amount to move object with</param>
        /// <param name="space">world of local axis</param>
        /// <param name="removeIfBelowMap">remove the destroy the object if it is below the map</param>
        public void Move(Vector3 addition, Space space = Space.World, bool removeIfBelowMap = true)
        {
            var speed = this._useMinSpeed ? this._minSpeed : this._maxSpeed;

            this.transform.Translate(addition * speed * Time.deltaTime, space);

            if (this._mapBounds.IsBelowBounds(this.transform.position.y + (this._spriteRenderer.sprite.bounds.size.y / 2)) && removeIfBelowMap)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }
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