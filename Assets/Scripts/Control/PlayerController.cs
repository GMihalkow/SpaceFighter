using SpaceFighter.Combat;
using SpaceFighter.Movement;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _shootTimeout = 0.15f;

        private Fighter _fighter;
        private Mover _mover;
        private float _timePassedSinceLastShot;

        private void Awake()
        {
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            this._mover.LookAt(Input.mousePosition);
            this.HandleCombat();
            this.HandleMovement();
        }

        private void HandleCombat()
        {
            this._mover.UseMinSpeed = Input.GetKey(KeyCode.Mouse0);

            if (Input.GetKey(KeyCode.Mouse0))
            {
                this._timePassedSinceLastShot += Time.unscaledDeltaTime;
                
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