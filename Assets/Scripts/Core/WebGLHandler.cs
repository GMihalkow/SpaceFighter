using System.Runtime.InteropServices;
using UnityEngine;

namespace SpaceFighter.Core
{
    public class WebGLHandler : MonoBehaviour
    {
        [DllImport("__Internal")]
        public static extern bool IsMobileBrowser();
    }
}