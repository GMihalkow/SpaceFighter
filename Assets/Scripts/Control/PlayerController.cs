using SpaceFighter.Combat;
using SpaceFighter.Core;
using SpaceFighter.Movement;
using SpaceFighter.Obstacles;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        private const float MAX_DISTANCE = 1000f;

        [SerializeField] Shield _shieldPrefab;
        [SerializeField] float _shootTimeout = 0.15f;
        [SerializeField] bl_Joystick _mobileJoystick;

        private bool _shieldIsAvailable;
        private bool _gameIsPaused;
        private bool _hasClickedUI;
        private Health _health;
        private Fighter _fighter;
        private Mover _mover;
        private float _timePassedSinceLastShot;
        private Shield _shieldInstance;

        private void Awake()
        {
            this._shieldIsAvailable = true;
            this._health = this.GetComponent<Health>();
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            if (this._health.IsDead || this._gameIsPaused) return;
            if (!this._hasClickedUI) this._mover.LookAt(Input.mousePosition);

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
            if ((!Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyUp(KeyCode.Mouse0)) || !Platform.IsMobileBrowser) return;

            var hitInfo = this.GetRaycast();
            var isMobileUI = hitInfo.collider?.CompareTag("MobileJoystick") == true || this._hasClickedUI;

            if (Input.GetKeyDown(KeyCode.Mouse0) && isMobileUI)
            {
                this._hasClickedUI = isMobileUI;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && this._hasClickedUI && isMobileUI)
            {
                this._hasClickedUI = false;
            }
        }

        private void HandleCombat()
        {
            var shieldIsDeactivated = this._shieldInstance == null || !this._shieldInstance.gameObject.activeSelf;

            this._mover.UseMinSpeed = Input.GetKey(KeyCode.Mouse0) && shieldIsDeactivated;

            if (Input.GetKeyDown(KeyCode.Mouse0) && shieldIsDeactivated)
            {
                this._timePassedSinceLastShot = this._shootTimeout;

                var hitInfo = this.GetRaycast();

                if (hitInfo.collider?.CompareTag("MobileJoystick") != true) this._mover.LookAt(Input.mousePosition);
            }

            if (Input.GetKey(KeyCode.Mouse0) && shieldIsDeactivated)
            {
                this._timePassedSinceLastShot += Time.deltaTime;
                
                if ((Mathf.Approximately(this._timePassedSinceLastShot, this._shootTimeout) || this._timePassedSinceLastShot >= this._shootTimeout) && !this._hasClickedUI)
                {
                    this._fighter.Shoot();
                    this._timePassedSinceLastShot = 0f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && this._shieldIsAvailable)
            {
                this.CreateShield();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1) && this._shieldIsAvailable)
            {
                this.HideShield();
            }
            else
            {
                this._timePassedSinceLastShot = 0f;
            }
        }

        private void HandleMovement()
        {
            var xAxis = Platform.IsMobileBrowser ? this._mobileJoystick.Horizontal : Input.GetAxis("Horizontal");
            var yAxis = Platform.IsMobileBrowser ? this._mobileJoystick.Vertical : Input.GetAxis("Vertical");

            this._mover.MoveInBounds(new Vector3(xAxis, yAxis), isPlayer: true);
        }

        private void CreateShield(bool isActive = true)
        {
            this._shieldIsAvailable = true;

            if (this._shieldInstance != null)
            {
                this._shieldInstance.gameObject.SetActive(true);
                return;
            }

            this._shieldInstance = GameObject.Instantiate(this._shieldPrefab.gameObject).GetComponent<Shield>();
            this._shieldInstance.gameObject.SetActive(isActive);

            this._shieldInstance.SetTarget(this.gameObject);
            this._shieldInstance.GetComponent<Health>().OnDeath.AddListener(() => this._shieldIsAvailable = false);
        }

        private void HideShield()
        {
            if (this._shieldInstance == null) return;

            this._shieldInstance.ResetFade();
            this._shieldInstance.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("ShieldRestore") || this._shieldInstance?.HealthIsFull == true) return;

            if (this._shieldInstance == null)
            {
                this.CreateShield(false);
            } 
            else
            {
                this._shieldInstance.GetComponent<Health>().ResetHealth();
            }

            GameObject.Destroy(collision.gameObject);
        }

        private RaycastHit2D GetRaycast()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, MAX_DISTANCE);

            return hitInfo;
        }
    }
}