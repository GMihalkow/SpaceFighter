using UnityEngine;

namespace SpaceFighter.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] GameObject _objectToFollow;

        private void LateUpdate()
        {
            var pos = this._objectToFollow.transform.position;
            pos.z = this.transform.position.z;

            this.transform.position = pos;
        }
    }
}