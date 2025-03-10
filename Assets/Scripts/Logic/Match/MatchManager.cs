using Circles;
using Logic.Points;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Match
{
    public class MatchManager : MonoBehaviour
    {
        [SerializeField] private VerificationPoint[] _verificationPoints;
        [SerializeField] private PointsManager _pointsManager;

        public event Action Lost;

        public void CheckAllMatches()
        {
            HashSet<Circle> circlesToDestroy = new HashSet<Circle>();

            foreach (var point in _verificationPoints)
            {
                List<Circle> matchedCircles = point.CheckForMatch();

                if (matchedCircles != null)
                {
                    circlesToDestroy.UnionWith(matchedCircles);
                    _pointsManager.AddPointsForMatch(matchedCircles[0].Color);
                }
            }

            foreach (var circle in circlesToDestroy)
                circle.Destroy();

            StartCoroutine(WaitAndCheckGameOver());
        }

        private IEnumerator WaitAndCheckGameOver()
        {
            yield return new WaitForSeconds(0.1f);

            bool allFull = true;

            foreach (var point in _verificationPoints)
            {
                if (!point.IsFull() || point.IsClearingCircles)
                {
                    allFull = false;
                    break;
                }
            }

            if (allFull)
            {
                ClearCircles();
                Lost?.Invoke();
            }
        }

        private void ClearCircles()
        {
            foreach (var circle in FindObjectsOfType<Circle>())
                Destroy(circle.gameObject);
        }
    }
}