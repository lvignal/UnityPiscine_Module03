using Module03.Base;
using Module03.Enemy;
using Module03.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module03
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BaseController _baseController;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PauseMenu _pauseMenu;

        private EndLevelScreen _endLevelScreen;
        private static int _currentLevel = 1;
        
        private bool _isCurrentLevelWon = false;
        private int _numberOfLevels = 2;

        private void Awake()
        {
            _baseController.OnAllHPLost += LaunchGameOver;
            _enemySpawner.OnEnemyKilled += GiveEnergyToBase;
            _enemySpawner.OnMaxNumberOfEnemiesReached += LaunchVictory;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _pauseMenu.Pause();
        }

        private void LaunchGameOver()
        {
            Debug.Log("Game Over");
            EndLevel(false);
        }
        
        private void LaunchVictory()
        {
            EndLevel(true);
        }

        private void EndLevel(bool hasWon)
        {
            _isCurrentLevelWon = hasWon;
            _enemySpawner.StopSpawning();
            SceneManager.LoadScene("Score");
            SceneManager.sceneLoaded += InitializeEndLevelScreen;
        }
        
        private void InitializeEndLevelScreen(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= InitializeEndLevelScreen;
            _endLevelScreen = FindObjectOfType<EndLevelScreen>();
            if (_endLevelScreen == null)
                return;
            
            _endLevelScreen.OnButtonClicked += ReactToEndLevelButtonClicked;
            _endLevelScreen.Initialize(_isCurrentLevelWon, _currentLevel == _numberOfLevels, 
                _baseController.HealthPoints, _baseController.EnergyPoints, _enemySpawner.NumberOfEnemiesKilled);
            _endLevelScreen.HighlightRank(CalculateRank());
        }

        private int CalculateRank()
        {
            int rank = 0;
            if (!_isCurrentLevelWon)
                return rank;
            
            if (_baseController.HealthPoints > 0 && _baseController.HealthPoints <= 2)
                rank = 1;
            else if (_baseController.HealthPoints > 2 && _baseController.HealthPoints <= 4)
                rank = 2;
            else if (_baseController.HealthPoints == 5)
                rank = 3;
            
            if (_baseController.EnergyPoints >= 50 && _baseController.EnergyPoints < 200)
                rank++;
            else if (_baseController.EnergyPoints >= 200)
                rank += 2;

            return rank;
        }

        private void ReactToEndLevelButtonClicked()
        {
            _endLevelScreen.OnButtonClicked -= ReactToEndLevelButtonClicked;
            if (_isCurrentLevelWon)
                GoToNextLevel();
            else
                RestartLevel();
        }
        
        private void RestartLevel()
        {
            SceneManager.LoadScene(_currentLevel);
        }
        
        private void GoToNextLevel()
        {
            _currentLevel++;

            if (_currentLevel > _numberOfLevels)
            {
                _currentLevel = 1;
                SceneManager.LoadScene("EndGame");
            }
            else
                SceneManager.LoadScene(_currentLevel);
        }
        
        private void GiveEnergyToBase()
        {
            _baseController.AddEnergy(10);
        }
        
        private void OnDestroy()
        {
            _baseController.OnAllHPLost -= LaunchGameOver;
            _enemySpawner.OnEnemyKilled -= GiveEnergyToBase;
            _enemySpawner.OnMaxNumberOfEnemiesReached -= LaunchVictory;
        }
    }
}