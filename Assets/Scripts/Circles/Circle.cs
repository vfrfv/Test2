using Logic;
using Logic.Match;
using System.Collections;
using UnityEngine;

namespace Circles
{
    public class Circle : MonoBehaviour
    {
        [SerializeField] private CircleColor _color;
        [SerializeField] private ParticleSystem _explosionEffect;

        private MatchManager _matchManager;
        private SpriteRenderer _spriteRenderer;

        public CircleColor Color => _color;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Circle>() != null || collision.gameObject.GetComponent<Earth>() != null)
            {
                _matchManager.CheckAllMatches();
            }
        }

        public void InitializeMatchFinder(MatchManager matchFinder) => _matchManager = matchFinder;

        public void Destroy() => StartCoroutine(FadeOutAndDestroy());

        private IEnumerator FadeOutAndDestroy()
        {
            _explosionEffect.Play();

            float fadeDuration = _explosionEffect.main.duration;
            float elapsedTime = 0f;
            Color startColor = _spriteRenderer.color;

            while (elapsedTime < fadeDuration)
            {
                _spriteRenderer.color = new Color(
                    startColor.r,
                    startColor.g,
                    startColor.b,
                    Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration)
                );

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
            Destroy(gameObject);
        }
    }
}