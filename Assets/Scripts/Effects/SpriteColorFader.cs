using System.Collections;
using UnityEngine;

namespace SpaceFighter.Effects
{
    public class SpriteColorFader : MonoBehaviour
    {
        [SerializeField] float _duration = 1f;
        [SerializeField] float _speed = 5f;
        [SerializeField] Color _fadeInColor = Color.red;
        [SerializeField] Color _fadeOutColor = Color.white;

        private SpriteRenderer _spriteRenderer;
        private float _timeout;

        private void Awake()
        {
            this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        }

        /// <summary>
        /// Called from editor
        /// </summary>
        public void Fade()
        { 
            this.StopAllCoroutines();   
            this.StartCoroutine(this.FadeInOutCoroutine());
        }

        private IEnumerator FadeInOutCoroutine()
        {
            this._timeout = 0f;

            yield return this.FadeCoroutine(this._fadeOutColor, this._fadeInColor);
            
            this._timeout = 0f;

            yield return this.FadeCoroutine(this._fadeInColor, this._fadeOutColor);
        }

        private IEnumerator FadeCoroutine(Color fadeInColor, Color fadeOutColor)
        {
            while (!Mathf.Approximately(this._timeout, this._duration) || this._timeout < this._duration)
            {
                this._spriteRenderer.color = Color.Lerp(fadeOutColor, fadeInColor, this._timeout / this._duration);
                this._timeout += (Time.deltaTime * this._speed);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}