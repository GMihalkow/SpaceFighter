using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] GameObject[] _enemyPrefabs;

        public override void Spawn(Vector2 position)
        {
            var prefab = this.GetRandomPrefab();

            GameObject.Instantiate(prefab, position, prefab.transform.rotation);
        }

        private GameObject GetRandomPrefab()
        {
            if (this._enemyPrefabs.Length == 0) throw new System.InvalidOperationException("No prefabs specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._enemyPrefabs.Length - 1));

            return this._enemyPrefabs[randomIndex];
        }
    }
}