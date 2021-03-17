using UnityEngine;

namespace SpaceFighter.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GameObject _projectilePrefab;

        public void Shoot()
        {
            var projectile = GameObject.Instantiate(this._projectilePrefab, this.transform.position, Quaternion.identity);
        }
    }
}