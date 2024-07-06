using Module03.Enemy;
using UnityEngine;

namespace Module03.Turret
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private float _basicsDamages;
        [SerializeField] private GameObject _turret;
        [SerializeField] private TurretDetectionZone _detectionZone;
        [SerializeField] private GameObject _bulletPrefab;

        private EnemyController _enemyTargeted;
        private float _timer;
        private float _bulletSpeed = 10.0f;
        
        private void Update()
        {
            if (_detectionZone.EnemiesInZone.Count == 0)
                return;
            
            TargetClosestEnemy();
            
            if (_enemyTargeted != null)
            {
                _timer += Time.deltaTime;
                if (_timer >= _fireRate)
                {
                    Fire();
                    _timer = 0.0f;
                }
            }
        }

        private void TargetClosestEnemy()
        {
            GameObject closestEnemy = _detectionZone.EnemiesInZone[0].gameObject;
            foreach (EnemyController enemy in _detectionZone.EnemiesInZone)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
                    closestEnemy = enemy.gameObject;
            }
            TargetEnemy(closestEnemy);
        }
        
        private void TargetEnemy(GameObject enemy)
        { 
            _enemyTargeted = enemy.GetComponent<EnemyController>();
            _enemyTargeted.OnDestroyed += StopTargetEnemy;
        }
        
        private void StopTargetEnemy()
        {
            _enemyTargeted = null;
        }

        private void Fire()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _turret.transform.position, transform.rotation, transform);
            Vector3 direction = _enemyTargeted.transform.position - transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<BulletController>().TurretDamage = _basicsDamages;
        }
    }
}