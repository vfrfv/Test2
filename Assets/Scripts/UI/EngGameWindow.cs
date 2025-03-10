using Logic.Points;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EngGameWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textPoints;
        [SerializeField] private PointsManager _pointsManager;

        private void OnEnable() => ShowPoints();

        public void ShowPoints() => _textPoints.text = $"Ты набрал {_pointsManager.CurrentPoints} очков";
    }
}