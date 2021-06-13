using SpaceFighter.Movement;
using System.Collections;
using UnityEngine;

namespace SpaceFighter.Effects
{
    public class Rumble : MonoBehaviour
    {
        [SerializeField] Vector3[] _directions; 
        [SerializeField] float _power = 1.5f;
        [SerializeField] bool _randomize = false;
        [SerializeField] float _timePerDirection = 0.12f;

        private Coroutine _rumbleCoroutine;
        private Mover _mover;

        private void Awake()
        {
            this._mover = this.GetComponent<Mover>();
        }

        public void BeginRumble()
        {
            if (this._rumbleCoroutine == null)
            {
                this._rumbleCoroutine = this.StartCoroutine(this.RumbleCoroutine());
            }
        }

        private IEnumerator RumbleCoroutine()
        {
            var timePassed = 0f;

            foreach (var dir in this._directions)
            {
                while (timePassed < this._timePerDirection)
                {
                    var power = this._randomize ? Random.Range(0f, this._power) : this._power;

                    this._mover.Move(dir * power, Space.Self);

                    timePassed += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }

                timePassed = 0f;
            }

            this._rumbleCoroutine = null;
        }
    }
}