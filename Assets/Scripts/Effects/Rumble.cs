using SpaceFighter.Movement;
using System.Collections;
using UnityEngine;

namespace SpaceFighter.Effects
{
    public class Rumble : MonoBehaviour
    {
        [SerializeField] float _power = 1.5f;
        [SerializeField] bool _randomize = false;
        [SerializeField] float _timePerDirection = 0.12f;

        private Mover _mover;

        private void Awake()
        {
            this._mover = this.GetComponent<Mover>();
        }

        public void BeginRumble()
        {
            this.StartCoroutine(this.RumbleCoroutine());
        }

        private IEnumerator RumbleCoroutine()
        {
            var timePassed = 0f;
            var directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.left, Vector3.right };

            foreach (var dir in directions)
            {
                while (timePassed < this._timePerDirection)
                {
                    var power = this._randomize ? Random.Range(0f, this._power) : this._power;

                    this._mover.Move(dir * power, Space.Self);

                    timePassed += Time.unscaledDeltaTime;

                    yield return new WaitForEndOfFrame();
                }

                timePassed = 0f;
            }
        }
    }
}