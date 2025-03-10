using Circles.AnimationCircles;
using Logic;
using Logic.Points;
using Logic.Spawner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIContriller : MonoBehaviour
    {
        [SerializeField] private Image _startMenu;
        [SerializeField] private Image _endGame;
        [SerializeField] private SpawnCircleAnimation _spawnCircleAnimation;
        [SerializeField] private CircleSpawner _circleSpawner;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitMenuButton;

        [SerializeField] private GameController _gameController;
        [SerializeField] private PointsManager _pointsManager;
        [SerializeField] private TMP_Text _currentPoints;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(UnplugMenu);
            _restartButton.onClick.AddListener(UnplugUngGame);
            _exitMenuButton.onClick.AddListener(ShowMenu);

            _pointsManager.OnPointsChanged += ShowCurrentPoints;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(UnplugMenu);
            _restartButton.onClick.RemoveListener(UnplugUngGame);
            _exitMenuButton.onClick.RemoveListener(ShowMenu);

            _pointsManager.OnPointsChanged -= ShowCurrentPoints;
        }

        public void ShowMenu()
        {
            _startMenu.gameObject.SetActive(true);
            _endGame.gameObject.SetActive(false);
            _spawnCircleAnimation.enabled = true;
        }

        public void ShowEndGame()
        {
            _endGame.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void UnplugMenu()
        {
            _startMenu.gameObject.SetActive(false);
            _pointsManager.ResetPoints();
            _spawnCircleAnimation.enabled = false;
            _circleSpawner.StartSpawnNewCircle();
            Time.timeScale = 1;
        }

        private void UnplugUngGame()
        {
            _endGame.gameObject.SetActive(false);
            _gameController.Restart();
            _circleSpawner.StartSpawnNewCircle();
            Time.timeScale = 1;
        }

        private void ShowCurrentPoints(int points) => _currentPoints.text = points.ToString();
    }
}