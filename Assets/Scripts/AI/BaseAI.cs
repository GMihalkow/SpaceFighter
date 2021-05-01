using SpaceFighter.Core;
using SpaceFighter.Movement;
using SpaceFighter.UI;
using UnityEngine;

namespace SpaceFighter.AI
{
    public abstract class BaseAI : MonoBehaviour
    {
        private Score _score;
        private Health _health;
        protected Mover _mover;
        protected GameObject _player;
        protected Vector2 _lastPlayerPos;

        protected virtual void Awake()
        {
            this._mover = this.GetComponent<Mover>();
            this._player = GameObject.FindGameObjectWithTag("Player");
            this._health = this.GetComponent<Health>();
            this._score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        }

        /// <summary>
        /// called by unity event upon enemy death
        /// </summary>
        public void OnDeath()
        {
            this._score.Increment();
            this._health.Explode();
        }

        protected void LookAtPlayer()
        {
            this._lastPlayerPos = Camera.main.WorldToScreenPoint(this._player.transform.position);
            this._mover.LookAt(this._lastPlayerPos);
        }
    }
}