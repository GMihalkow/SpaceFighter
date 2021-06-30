using UnityEngine;

namespace SpaceFighter.Spawn
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _container;
        [SerializeField] int _maxSpawnedObjects = 25;

        public bool CanSpawn => this._maxSpawnedObjects > this._container.transform.childCount;

        public abstract void Spawn(Vector2 position);
    }
}