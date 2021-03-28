using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] float _staticDamage = 5f;

        public float StaticDamage => this._staticDamage;
    }
}