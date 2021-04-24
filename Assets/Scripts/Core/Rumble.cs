using SpaceFighter.Movement;
using System.Collections;
using UnityEngine;

namespace SpaceFighter.Core
{
    public class Rumble : MonoBehaviour
    {
        [SerializeField] float _power = 1.5f;
        [SerializeField] bool _randomize = false;

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
            var frames = 12;
            var directions = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

            foreach (var dir in directions)
            {
                for (int i = 0; i <= frames; i++)
                {
                    var power = this._randomize ? Random.Range(0f, this._power) : this._power;

                    this._mover.Move(dir * power);

                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}