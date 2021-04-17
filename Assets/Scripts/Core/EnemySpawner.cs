using UnityEngine;

namespace SpaceFighter.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] _enemyPrefabs;
        [SerializeField] float _spawnFrequency = 3f;
        [SerializeField] float _topOffset = 2f;

        private MapBounds _mapBounds;
        private float _timePassed;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
        }

        private void Update()
        {
            this._timePassed += Time.unscaledDeltaTime;

            if (Mathf.Approximately(this._timePassed, this._spawnFrequency) || this._timePassed > this._spawnFrequency)
            {
                var prefab = this.GetRandomPrefab();

                GameObject.Instantiate(prefab, this._mapBounds.GeneratePositionAboveCamera(this._topOffset), prefab.transform.rotation);
                this._timePassed = 0f;
            }
        }

        private GameObject GetRandomPrefab()
        {
            if (this._enemyPrefabs.Length == 0) throw new System.InvalidOperationException("No prefabs specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._enemyPrefabs.Length - 1));

            return this._enemyPrefabs[randomIndex];
        }
    }
}