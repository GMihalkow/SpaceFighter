using UnityEngine;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _bar;
        private float _value = 1f;

        private void Awake()
        {
            this._bar = this.GetComponent<Slider>();
        }

        private void Update()
        {
            this._bar.value = this._value;
        }

        public void SetValue(float val)
        {
            this._value = val;
        }
    }
}