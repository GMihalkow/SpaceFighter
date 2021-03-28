using UnityEngine;

namespace SpaceFighter.Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject _containerPrefab;
        [SerializeField] float _spawnFrequency = 5f;
        [SerializeField] float _topOffset = 2f;
        [SerializeField] string _prefabsDirectory;

        private MapBounds _mapBounds;
        private float _deltaPassed;
        private Sprite[] _sprites;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._sprites = Resources.LoadAll<Sprite>(this._prefabsDirectory);
        }

        private void Update()
        {
            this._deltaPassed += Time.unscaledDeltaTime;

            if (Mathf.Approximately(this._deltaPassed, this._spawnFrequency) || this._deltaPassed > this._spawnFrequency)
            {
                var prefabInstance = GameObject.Instantiate(this._containerPrefab, this.GeneratePosition(), Quaternion.identity);
                prefabInstance.GetComponent<SpriteRenderer>().sprite = this.GetRandomSprite();
                prefabInstance.AddComponent<BoxCollider2D>().isTrigger = true;

                this._deltaPassed = 0f;
            }
        }

        private Sprite GetRandomSprite()
        {
            if (this._sprites.Length == 0) throw new System.InvalidOperationException("No spawn sprites specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._sprites.Length - 1));

            return this._sprites[randomIndex];
        }

        private Vector2 GeneratePosition()
        {
            var x = Random.Range(this._mapBounds.MinX, this._mapBounds.MaxX);

            return new Vector2(x, this._mapBounds.MaxY + this._topOffset);
        }
    }
}