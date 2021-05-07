using UnityEngine;

namespace SpaceFighter.AI
{
    public class KamikadzeAIController : BaseAI
    {
        private void Update()
        {
            if (this._playerHealth.IsDead) return;

            this.LookAtPlayer();
            this._mover.MoveInBounds(Vector3.right, Space.Self);
        }
    }
}