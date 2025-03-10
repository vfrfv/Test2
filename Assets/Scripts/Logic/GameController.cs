using Logic.Match;
using Logic.Points;
using UI;
using UnityEngine;

namespace Logic
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UIContriller _uIContriller;
        [SerializeField] private MatchManager _matchManager;
        [SerializeField] private PointsManager _pointsManager;
        [SerializeField] private EngGameWindow _engGameWindow;

        private void Start() => _uIContriller.ShowMenu();

        private void OnEnable() => _matchManager.Lost += ShowLoss;

        private void OnDisable() => _matchManager.Lost -= ShowLoss;

        public void Restart() => _pointsManager.ResetPoints();

        private void ShowLoss()
        {
            _uIContriller.ShowEndGame();
            _engGameWindow.ShowPoints();
        }
    }
}