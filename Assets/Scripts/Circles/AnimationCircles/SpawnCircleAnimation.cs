using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circles.AnimationCircles
{
    public class SpawnCircleAnimation : MonoBehaviour
    {
        private readonly List<FallingBall> FallingBalls = new List<FallingBall>();

        [SerializeField] private FallingBall[] _fallingBallPrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _delay = 0.1f;

        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            DestroyAllBalls();

            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator Spawn()
        {
            var amountDelay = new WaitForSeconds(_delay);

            while (true)
            {
                FallingBall fallingBall = Instantiate(
                    _fallingBallPrefabs[Random.Range(0, _fallingBallPrefabs.Length)],
                    _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position,
                    Quaternion.identity);

                FallingBalls.Add(fallingBall);
                yield return amountDelay;
            }
        }

        private void DestroyAllBalls()
        {
            foreach (var fallingBall in FallingBalls)
                Destroy(fallingBall.gameObject);

            FallingBalls.Clear();
        }
    }
}