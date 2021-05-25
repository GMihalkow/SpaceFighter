using UnityEngine;

namespace SpaceFighter.AI
{
    public class KamikadzeAIController : BaseAI
    {
        [SerializeField] float _slowDownTimeout = 1f;
        [SerializeField] float _speedFragmentWhenAttacked = 0.3f;

        private bool _isAttacked;
        private float _timePassedSinceSlowdown;

        private void Update()
        {
            if (this._playerHealth.IsDead) return;

            if (this._isAttacked && (Mathf.Approximately(this._timePassedSinceSlowdown, this._slowDownTimeout) || this._timePassedSinceSlowdown > this._slowDownTimeout))
            {
                this._timePassedSinceSlowdown = 0f;
                this._isAttacked = false;
            }
            else if(this._isAttacked)
            {
                this._timePassedSinceSlowdown += Time.deltaTime;
            }
            
            this.LookAtPlayer();

            var fragment = this._isAttacked ? this._speedFragmentWhenAttacked : 1f;
            this._mover.MoveInBounds(Vector3.right * fragment, Space.Self);
        }

        /// <summary>
        /// Called by event in editor
        /// </summary>
        public void SetAttackedState()
        {
            this._timePassedSinceSlowdown = 0f;
            this._isAttacked = true;
        }
    }
}