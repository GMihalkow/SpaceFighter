using UnityEngine;

namespace SpaceFighter.Core
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _containerPrefab;
        [SerializeField] float _spawnFrequency = 5f;
        [SerializeField] float _topOffset = 2f;
        [SerializeField] string _prefabsDirectory;
        [SerializeField] float _collidersScale = 0.8f;

        private MapBounds _mapBounds;
        private float _timePassed;
        private Sprite[] _sprites;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._sprites = Resources.LoadAll<Sprite>(this._prefabsDirectory);
        }

        private void Update()
        {
            this._timePassed += Time.unscaledDeltaTime;

            if (Mathf.Approximately(this._timePassed, this._spawnFrequency) || this._timePassed > this._spawnFrequency)
            {
                var prefabInstance = GameObject.Instantiate(this._containerPrefab, this._mapBounds.GeneratePositionAboveCamera(this._topOffset), Quaternion.identity);
                prefabInstance.GetComponent<SpriteRenderer>().sprite = this.GetRandomSprite();
                
                var boxCollider = prefabInstance.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
                boxCollider.size = new Vector2(boxCollider.size.x * this._collidersScale, boxCollider.size.y * this._collidersScale);

                this._timePassed = 0f;
            }
        }

        private Sprite GetRandomSprite()
        {
            if (this._sprites.Length == 0) throw new System.InvalidOperationException("No spawn sprites specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._sprites.Length - 1));

            return this._sprites[randomIndex];
        }
    }
}