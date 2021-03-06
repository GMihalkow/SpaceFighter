using SpaceFighter.Core;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] List<Spawner> _spawners;
        [SerializeField] float _spawnFrequency = 3f;
        [SerializeField] float _topOffset = 2f;

        private bool _isStopped;
        private MapBounds _mapBounds;
        private float _timePassed;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private void Update()
        {
            if (this._isStopped) return;

            this._timePassed += Time.deltaTime;

            if (Mathf.Approximately(this._timePassed, this._spawnFrequency) || this._timePassed > this._spawnFrequency)
            {
                var pos = this._mapBounds.GeneratePositionAboveCamera(this._topOffset);

                var spawner = this._spawners[0];
                this._spawners.Remove(spawner);

                if (spawner.CanSpawn)
                {
                    spawner.Spawn(pos);
                }

                this._spawners.Add(spawner);

                this._timePassed = 0f;
            }
        }

        public void Stop()
        {
            this._isStopped = true;
        }
    }
}