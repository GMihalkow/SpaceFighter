using SpaceFighter.Combat;
using SpaceFighter.Core;
using SpaceFighter.Movement;
using SpaceFighter.Obstacles;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _shootTimeout = 0.15f;

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
            if (this._health.IsDead) return;

            this._mover.LookAt(Input.mousePosition);
            this.HandleCombat();
            this.HandleMovement();
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

        private void HandleCombat()
        {
            this._mover.UseMinSpeed = Input.GetKey(KeyCode.Mouse0);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this._fighter.Shoot();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                this._timePassedSinceLastShot += Time.deltaTime;
                
                if (Mathf.Approximately(this._timePassedSinceLastShot, this._shootTimeout) || this._timePassedSinceLastShot >= this._shootTimeout)
                {
                    this._fighter.Shoot();
                    this._timePassedSinceLastShot = 0f;
                }
            }
            else
            {
                this._timePassedSinceLastShot = 0f;
            }
        }

        private void HandleMovement()
        {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            this._mover.MoveInBounds(new Vector3(xAxis, yAxis));
        }
    }
}