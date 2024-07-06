using System;
using Module03.Turret;
using UnityEngine;

namespace Module03.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private float _healthPoints;
        private float _speed;
        
        private Transform[] _waypoints;
        private int _currentWaypointIndex = 0;
        
        public Action OnDestroyed;
        public Action OnKilledByBullet;
        
        public void Initialize(int healthPoints, float speed, Transform[] waypoints)
        {
            _healthPoints = healthPoints;
            _speed = speed;
            _waypoints = waypoints;
        }
        
        private void Update()
        {
            if (_waypoints == null || _waypoints.Length == 0 || _currentWaypointIndex >= _waypoints.Length)
                return;
            
            Vector3 targetPosition = _waypoints[_currentWaypointIndex].position;
            Vector3 moveDirection = targetPosition - transform.position;
            transform.Translate(moveDirection.normalized * _speed * Time.deltaTime);
            
            // when the enemy reaches the waypoint, it moves to the next one
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                _currentWaypointIndex++;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Bullet")
                return;
            
            float turretDamage = other.gameObject.GetComponent<BulletController>().TurretDamage;
            _healthPoints -= (turretDamage + 1);
            Debug.Log("Enemy HP = " + _healthPoints);
            Destroy(other.gameObject);
            if (_healthPoints <= 0)
            {
                Debug.Log("Enemy killed !");
                Destroy(gameObject);
                OnKilledByBullet?.Invoke();
            }
        }
        
        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}