using UnityEngine;

namespace SpaceFighter.Spawn
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _container;

        public abstract void Spawn(Vector2 position);
    }
}