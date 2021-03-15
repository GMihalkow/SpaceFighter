using UnityEngine;

namespace SpaceFighter.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;

        private void Awake()
        {
            this._mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            this.HandleMovement();
        }

        private void HandleMovement()
        {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            this._mover.Move(new Vector3(xAxis, yAxis));
        }
    }
}