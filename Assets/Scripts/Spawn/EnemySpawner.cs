using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] GameObject[] _enemyPrefabs;

        private int _lastIndex = -1;

        public override void Spawn(Vector2 position)
        {
            var prefab = this.GetRandomPrefab();

            GameObject.Instantiate(prefab, position, prefab.transform.rotation, this._container.transform);
        }

        private GameObject GetRandomPrefab()
        {
            if (this._enemyPrefabs.Length == 0) throw new System.InvalidOperationException("No prefabs specified.");

            var filteredIndexes = this.ExcludeIndex(this._lastIndex);
            var filteredIndex = Mathf.FloorToInt(Random.Range(0, filteredIndexes.Count));

            this._lastIndex = filteredIndexes[filteredIndex];

            return this._enemyPrefabs[this._lastIndex];
        }

        private List<int> ExcludeIndex(int index)
        {
            var list = new List<int>();

            for (int i = 0; i < this._enemyPrefabs.Length; i++)
            {
                if (index == i) continue;
                list.Add(i);
            }

            return list;
        }
    }
}