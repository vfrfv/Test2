using Circles;
using System;
using UnityEngine;

namespace Logic.Points
{
    public class PointsManager : MonoBehaviour
    {
        private readonly int PointsGreenCircles = 2;
        private readonly int PointsBlueCircles = 4;
        private readonly int PointsRedCircles = 6;

        private int _currentPoints;

        public int CurrentPoints => _currentPoints;

        public event Action<int> OnPointsChanged;

        private void Start()
        {
            ResetPoints();
        }

        public void AddPointsForMatch(CircleColor color)
        {
            int pointsToAdd = 0;

            switch (color)
            {
                case CircleColor.Green:
                    pointsToAdd = PointsGreenCircles;
                    break;

                case CircleColor.Blue:
                    pointsToAdd = PointsBlueCircles;
                    break;

                case CircleColor.Red:
                    pointsToAdd = PointsRedCircles;
                    break;
            }

            _currentPoints += pointsToAdd;
            OnPointsChanged?.Invoke(_currentPoints);
        }

        public void ResetPoints()
        {
            _currentPoints = 0;
            OnPointsChanged?.Invoke(_currentPoints);
        }
    }
}