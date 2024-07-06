using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module03.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Vector3 _spawnPosition;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _maxNumberOfSpawnedEnemies = 20;
        [SerializeField] private int _enemyHP = 3;
        [SerializeField] private float _enemySpeed = 1f;
        [SerializeField] private Transform[] _waypoints;
        
        private List<GameObject> _enemiesList = new();
        private int _totalNumberOfSpawnedEnemies = 0;
        public Action OnEnemyKilled;
        public Action OnMaxNumberOfEnemiesReached;
        
        private float _timer = 0.0f;
        
        public int NumberOfEnemiesKilled { get; private set; }

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnRate)
            {
                _timer = 0.0f;
                SpawnEnemy();
            }
        }
        
        private void SpawnEnemy()
        {
            GameObject enemy = Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity);
            enemy.transform.SetParent(transform);
            _enemiesList.Add(enemy);
            _totalNumberOfSpawnedEnemies++;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Initialize(_enemyHP, _enemySpeed, _waypoints);
            enemyController.OnDestroyed += RemoveEnemyFromList;
            enemyController.OnKilledByBullet += InvokeEnemyKilled;

            if (_totalNumberOfSpawnedEnemies >= _maxNumberOfSpawnedEnemies)
            {
                OnMaxNumberOfEnemiesReached?.Invoke();
                enabled = false;
            }
        }

        public void StopSpawning()
        {
            enabled = false; // it will stop the Update method
            foreach (var enemy in _enemiesList)
            {
                Destroy(enemy);
            }
            _enemiesList.Clear();
        }
        
        private void RemoveEnemyFromList()
        {
            _enemiesList.RemoveAll(enemy => enemy == null);
        }
        
        private void InvokeEnemyKilled()
        {
            NumberOfEnemiesKilled++;
            OnEnemyKilled?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (GameObject enemy in _enemiesList)
            {
                if (enemy != null)
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.OnDestroyed -= RemoveEnemyFromList;
                    enemyController.OnKilledByBullet -= InvokeEnemyKilled;
                }
            }
            _enemiesList.Clear();
        }
    }
}
