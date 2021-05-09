using SpaceFighter.Core;
using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class MenuItemsSpawner : Spawner
    {
        [SerializeField] GameObject _containerPrefab;
        [SerializeField] string _spritesDirectory = "Planets/";
        [SerializeField] float _spawnFrequency = 15f;
        [SerializeField] float _spawnOffset = 3f;

        private float _spawnTimeout;
        private MapBounds _mapBounds;
        private Sprite[] _sprites;

        private void Awake()
        {
            this._mapBounds = Camera.main.GetComponent<MapBounds>();
            this._sprites = Resources.LoadAll<Sprite>(this._spritesDirectory);
        }

        private void Update()
        {
            if (Mathf.Approximately(this._spawnTimeout, this._spawnFrequency) || this._spawnTimeout >= this._spawnFrequency)
            {
                var pos = this._mapBounds.GeneratePositionLeftOfCamera(this._spawnOffset);
                
                this.Spawn(pos);

                this._spawnTimeout = 0f;
            }

            this._spawnTimeout += Time.deltaTime;
        }

        public override void Spawn(Vector2 position)
        {
            var container = GameObject.Instantiate(this._containerPrefab, position, Quaternion.identity);

            var spriteRenderer = container.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.GetRandomSprite();

            container.transform.Translate(Vector3.left * spriteRenderer.bounds.size.x / 2);
        }

        private Sprite GetRandomSprite()
        {
            if (this._sprites.Length == 0) throw new System.InvalidOperationException("No spawn sprites specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._sprites.Length - 1));

            return this._sprites[randomIndex];
        }
    }
}