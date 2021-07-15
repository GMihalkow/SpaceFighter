using System.Collections;
using UnityEngine;

namespace SpaceFighter.Effects
{
    public class SpriteColorFader : MonoBehaviour
    {
        [SerializeField] float _duration = 1f;
        [SerializeField] Color _fadeInColor = Color.red;
        [SerializeField] Color _fadeOutColor = Color.white;

        private SpriteRenderer _spriteRenderer;
        private float _timeout;

        private void Awake()
        {
            this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        }

        public void SetToInitialColor()
        {
            this._spriteRenderer.color = this._fadeOutColor;
        }

        /// <summary>
        /// Called from editor
        /// </summary>
        public void Fade()
        { 
            this.StopAllCoroutines();
            this.StartCoroutine(this.FadeInOutCoroutine());
        }

        public IEnumerator FadeInOutCoroutine()
        {
            this._timeout = 0f;

            yield return this.FadeCoroutine(this._fadeOutColor, this._fadeInColor);
            
            this._timeout = 0f;
                
            yield return this.FadeCoroutine(this._fadeInColor, this._fadeOutColor);
        }

        private IEnumerator FadeCoroutine(Color fadeInColor, Color fadeOutColor)
        {
            while (!Mathf.Approximately(this._timeout, this._duration) && this._timeout < this._duration && fadeInColor != fadeOutColor)
            {
                this._timeout = Mathf.Clamp(this._timeout + Time.deltaTime, 0f, this._duration);

                this._spriteRenderer.color = Color.Lerp(fadeInColor, fadeOutColor, this._timeout / this._duration);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}