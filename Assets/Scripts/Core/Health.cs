using UnityEngine;

namespace SpaceFighter.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _healthPoints = 100f;
        
        public void TakeDamage(float damage)
        {
            if (this._healthPoints <= 0) return;

            this._healthPoints = Mathf.Max(0, this._healthPoints - damage);
        }
    }
}