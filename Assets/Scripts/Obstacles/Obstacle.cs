using UnityEngine;

namespace SpaceFighter.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] GameObject _explosionEffectPrefab;
        [SerializeField] float _staticDamage = 5f;

        public float StaticDamage => this._staticDamage;

        public void Explode()
        {
            GameObject.Instantiate(this._explosionEffectPrefab, this.transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }
}