using UnityEngine;

namespace SpaceFighter.Spawn
{
    public class ObstacleSpawner : Spawner
    {
        [SerializeField] string _prefabsDirectory;
        [SerializeField] float _collidersScale = 0.8f;

        private Sprite[] _sprites;

        private void Awake()
        {
            this._sprites = Resources.LoadAll<Sprite>(this._prefabsDirectory);
        }

        public override void Spawn(Vector2 position)
        {
            var prefabInstance = GameObject.Instantiate(this._container, position, Quaternion.identity);
            prefabInstance.GetComponent<SpriteRenderer>().sprite = this.GetRandomSprite();

            var boxCollider = prefabInstance.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(boxCollider.size.x * this._collidersScale, boxCollider.size.y * this._collidersScale);
        }

        private Sprite GetRandomSprite()
        {
            if (this._sprites.Length == 0) throw new System.InvalidOperationException("No spawn sprites specified.");

            var randomIndex = Mathf.FloorToInt(Random.Range(0, this._sprites.Length - 1));

            return this._sprites[randomIndex];
        }
    }
}