using UnityEngine;

namespace SpaceFighter.Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] bool _recalculateResolution;

        private void Awake()
        {
            if (!this._recalculateResolution) return;

            Screen.SetResolution(Screen.width, Screen.height, true);
        }
    }
}