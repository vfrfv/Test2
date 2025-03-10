using UnityEngine;

namespace Circles.AnimationCircles
{
    public class FallingBall : MonoBehaviour
    {
        [SerializeField] private float bounceForce = 5f;
        [SerializeField] private float horizontalForce = 2f;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            float forceY = Random.Range(0, bounceForce);
            float forceX = Random.Range(-horizontalForce, horizontalForce);

            _rigidbody2D.velocity = new Vector2(forceX, forceY);
        }
    }
}