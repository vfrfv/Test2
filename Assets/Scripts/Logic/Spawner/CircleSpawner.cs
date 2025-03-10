using Circles;
using Logic.Match;
using System.Collections;
using UnityEngine;

namespace Logic.Spawner
{
    public class CircleSpawner : MonoBehaviour
    {
        [SerializeField] private Circle[] _circlePrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private FixedJoint2D _fixedJoint;
        [SerializeField] private float _amountDelay = 0.1f;
        [SerializeField] private MatchManager _matchManager;

        private Circle _currentCircle;
        private Coroutine _coroutine;

        //private void Start()
        //{
        //    if (_coroutine != null)
        //        StopCoroutine(_coroutine);

        //    _coroutine = StartCoroutine(SpawnNewCircle());
        //}

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                DetachCurrentCircle();
        }

        public void StartSpawnNewCircle()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SpawnNewCircle());
        }

        private IEnumerator SpawnNewCircle()
        {
            var delay = new WaitForSeconds(_amountDelay);
            yield return delay;

            int randomIndex = Random.Range(0, _circlePrefabs.Length);
            _currentCircle = Instantiate(_circlePrefabs[randomIndex], _spawnPoint.position, Quaternion.identity);
            _currentCircle.InitializeMatchFinder(_matchManager);
            _fixedJoint.connectedBody = _currentCircle.gameObject.GetComponent<Rigidbody2D>();
        }

        private void DetachCurrentCircle()
        {
            if (_currentCircle != null)
                _fixedJoint.connectedBody = null;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SpawnNewCircle());
        }
    }
}