using SpaceFighter.Combat;
using SpaceFighter.Movement;
using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter _fighter;
        private Mover _mover;

        private void Awake()
        {
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            this.HandleCombat();
            this.HandleMovement();
        }

        private void HandleCombat()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._fighter.Shoot();
            }
        }

        private void HandleMovement()
        {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            this._mover.Move(new Vector3(xAxis, yAxis));
        }
    }
}