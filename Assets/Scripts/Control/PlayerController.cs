using SpaceFighter.Combat;
using SpaceFighter.Core;
using SpaceFighter.Movement;
using SpaceFighter.Obstacles;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        private const float MAX_DISTANCE = 100f;

        [SerializeField] float _shootTimeout = 0.15f;
        [SerializeField] bl_Joystick _mobileJoystick;

        private int? _attackTouchId;
        private int? _movementTouchId;
        private bool _gameIsPaused;
        private Health _health;
        private Fighter _fighter;
        private Mover _mover;
        private float _timePassedSinceLastShot;

        private void Awake()
        {
            this._health = this.GetComponent<Health>();
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            if (this._health.IsDead || this._gameIsPaused) return;

            this.HandleUI();
            this.HandleCombat();
            this.HandleMovement();
        }

        public bool TogglePausedState()
        {
            this._gameIsPaused = !this._gameIsPaused;

            return this._gameIsPaused;
        }

        /// <summary>
        /// Called on player's death
        /// </summary>
        public void OnDeath()
        {
            this._health.Explode(false);

            this.GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Destroy(this.GetComponent<ObstacleTarget>());

            var renderer = this.GetComponentInChildren<SpriteRenderer>();
            if (renderer == null) return;

            renderer.gameObject.SetActive(false);
        }

        private void HandleUI()
        {
            if (Input.touchCount <= 0 || !Platform.IsMobile) return;

            var movementTouch = default(Touch?);

            if (this._movementTouchId.HasValue) movementTouch = this.GetTouchById(this._movementTouchId.Value);

            if (movementTouch?.phase == TouchPhase.Ended || movementTouch?.phase == TouchPhase.Canceled)
            {
                this._movementTouchId = null;
            }

            var touches = this.GetTouchesNotEnded();

            if (!(touches?.Length > 0)) return;

            foreach (var touch in touches)
            {
                if (!touch.HasValue) continue;
                if (this._attackTouchId.HasValue && touch?.fingerId == this._attackTouchId) continue;

                // TODO [GM]: reuse raycasts?
                var hitInfo = this.GetRaycast(touch.Value.position);
                var isMobileUI = hitInfo.collider?.CompareTag("MobileJoystick") == true;

                if (!isMobileUI) continue;

                this._movementTouchId = touch.Value.fingerId;

                break;
            }
        }

        private void HandleCombat()
        {
            if (Input.touchCount <= 0 || !Platform.IsMobile) return;

            var attackTouch = default(Touch?);

            if (this._attackTouchId.HasValue) attackTouch = this.GetTouchById(this._attackTouchId.Value);

            if (attackTouch?.phase == TouchPhase.Ended || attackTouch?.phase == TouchPhase.Canceled)
            {
                attackTouch = null;
                this._attackTouchId = null;
            }

            // TODO [GM]: extract and reuse code in HandleCombat & HandleUI?
            var touches = this.GetTouchesNotEnded();

            if (!(touches?.Length > 0)) return;

            foreach (var touch in touches)
            {
                if (!touch.HasValue) continue;
                if (this._movementTouchId.HasValue && touch?.fingerId == this._movementTouchId) continue;

                var hitInfo = this.GetRaycast(touch.Value.position);
                var isMobileUI = hitInfo.collider?.CompareTag("MobileJoystick") == true;

                if (isMobileUI) continue;

                this._attackTouchId = touch.Value.fingerId;
                attackTouch = touch;
                break;
            }

            if (!this._attackTouchId.HasValue) return;

            // TODO [GM]: fix flag (set it to false somewhere)
            this._mover.UseMinSpeed = true;
            this._mover.LookAt(attackTouch.Value.position);

            this._timePassedSinceLastShot += Time.deltaTime;

            if ((Mathf.Approximately(this._timePassedSinceLastShot, this._shootTimeout) || this._timePassedSinceLastShot >= this._shootTimeout))
            {
                this._fighter.Shoot();
                this._timePassedSinceLastShot = 0f;
            }
        }

        private void HandleMovement()
        {
            var xAxis = Platform.IsMobile ? this._mobileJoystick.Horizontal : Input.GetAxis("Horizontal");
            var yAxis = Platform.IsMobile ? this._mobileJoystick.Vertical : Input.GetAxis("Vertical");

            this._mover.MoveInBounds(new Vector3(xAxis, yAxis), isPlayer: true);
        }

        private RaycastHit2D GetRaycast(Vector3 position)
        {
            var ray = Camera.main.ScreenPointToRay(position);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, MAX_DISTANCE);

            return hitInfo;
        }

        private Touch?[] GetTouchesNotEnded()
        {
            if (Input.touchCount <= 0 || !Platform.IsMobile) return null;

            var touches = new Touch?[Input.touchCount];

            for (int index = 0; index < Input.touchCount; index++)
            {
                var touch = Input.GetTouch(index);

                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) touches[index] = touch;
            }

            return touches;
        }

        private Touch? GetTouchById(int fingerId)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.fingerId != fingerId) continue;

                return touch;
            }

            return null;
        }
    }
}