using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class StarBurstSpawner : Spawner
    {
        [SerializeField] GameObject _burstPrefab;
        [SerializeField] float _maxSpawnFrequency = 15f;
        [SerializeField] float _minSpawnFrequency = 5f;

        private float _spawnTimeout;
        private float _currentSpawnFrequency;
        private MapBounds _mapBounds;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._currentSpawnFrequency = Random.Range(this._minSpawnFrequency, this._maxSpawnFrequency);
        }

        public override void Spawn(Vector2 position)
        {
            GameObject.Instantiate(this._burstPrefab, position, Quaternion.identity, this._container.transform);
        }

        private void Update()
        {
            if (!this.CanSpawn) return;

            if (this._spawnTimeout > this._currentSpawnFrequency || Mathf.Approximately(this._currentSpawnFrequency, this._spawnTimeout))
            {
                var pos = this._mapBounds.GenerateRandomPosition(true);
                
                this.Spawn(pos);

                this._spawnTimeout = 0f;
            }

            this._spawnTimeout += Time.deltaTime;
        }
    }
}