using UnityEngine;

namespace SpaceFighter.Spawn
{
    public abstract class Spawner : MonoBehaviour
    {
        public abstract void Spawn(Vector2 position);
    }
}