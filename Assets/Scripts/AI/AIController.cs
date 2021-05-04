using SpaceFighter.Combat;
using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.AI
{
    public class AIController : BaseAI
    {
        [SerializeField] float _visionDistance = 3f;
        [SerializeField] float _minAttackTimeout = 0.5f;
        [SerializeField] float _maxAttackTimeout = 1f;
        [SerializeField] float _patrolTimeout = 3f;

        private float _timePassedSincePatrolStart;
        private float _timePassedSinceAttack;
        private float _currentAttackTimeout;
        private Fighter _fighter;
        private MapBounds _mapBounds;
        private Quaternion _initialRotation;

        protected override void Awake()
        {
            base.Awake();

            this._initialRotation = this.transform.rotation;
            this._fighter = this.GetComponent<Fighter>();
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._currentAttackTimeout = Random.Range(this._minAttackTimeout, this._maxAttackTimeout);
        }

        private void Update()
        {
            var isInMapBounds = this._mapBounds.IsInBounds(this.transform.position);
            var playerIsInRange = Vector2.Distance(this.transform.position, this._player.transform.position) < this._visionDistance;

            if (playerIsInRange && isInMapBounds)
            {
                this.HandleCombat();
                this._timePassedSinceAttack += Time.unscaledDeltaTime;
            }
            else if (this._lastPlayerPos != default(Vector2) && isInMapBounds)
            {
                this.HandlePatrol();
            }
            else
            {
                this.HandleMovement();
                this._timePassedSinceAttack = 0f;
            }
        }

        /// <summary>
        /// Called from Unity event
        /// </summary>
        public void StartPatrolling()
        {
            this._timePassedSincePatrolStart = 0f;
            this.LookAtPlayer();
        }

        private void HandlePatrol()
        {
            this._mover.MoveInBounds(Vector3.right, Space.Self);
            this._timePassedSincePatrolStart += Time.unscaledDeltaTime;

            if (Mathf.Approximately(this._timePassedSincePatrolStart, this._patrolTimeout) || this._timePassedSincePatrolStart >= this._patrolTimeout)
            {
                this._lastPlayerPos = default(Vector2);
                this._timePassedSincePatrolStart = 0f;
            }
        }

        private void HandleCombat()
        {
            this.LookAtPlayer();

            if (Mathf.Approximately(this._timePassedSinceAttack, this._currentAttackTimeout) || this._timePassedSinceAttack >= this._currentAttackTimeout)
            {
                this._fighter.Shoot();
                this._timePassedSinceAttack = 0f;
                this._currentAttackTimeout = Random.Range(this._minAttackTimeout, this._maxAttackTimeout);
            }
        }

        private void HandleMovement()
        {
            this._mover.Move(Vector3.down);
            this.transform.rotation = this._initialRotation;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(this.transform.position, this._visionDistance);
        }
    }
}