using UnityEngine;

namespace SpaceFighter.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            this._rigidbody.AddForce(new Vector2(xAxis * this._speed, yAxis * this._speed), ForceMode2D.Impulse);
        }
    }
}