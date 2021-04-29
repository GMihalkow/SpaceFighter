using UnityEngine;
using UnityEngine.UI;

namespace SpaceFighter.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        private int _score;
        private Text _text;

        private void Awake()
        {
            this._text = this.GetComponent<Text>();
        }

        private void Start() => this.UpdateText();

        public void Increment() => this.UpdateText();

        private void UpdateText()
        {
            this._text.text = $"{this._score++}";
        }
    }
}