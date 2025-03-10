using UnityEngine;

namespace Logic
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private float _swingSpeed = 2f;
        [SerializeField] private float _maxAngle = 45f;

        private float _time;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            float angle = _maxAngle * Mathf.Sin(_swingSpeed * _time);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}