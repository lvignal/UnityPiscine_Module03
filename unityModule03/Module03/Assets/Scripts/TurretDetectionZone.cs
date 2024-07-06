using System.Collections.Generic;
using Module03.Enemy;
using UnityEngine;

namespace Module03.Turret
{
    public class TurretDetectionZone : MonoBehaviour
    {
        public List<EnemyController> EnemiesInZone = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                EnemiesInZone.Add(enemyController);
                enemyController.OnDestroyed += () => EnemiesInZone.Remove(enemyController);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController != null && EnemiesInZone.Contains(enemyController))
            {
                EnemiesInZone.Remove(enemyController);
            }
        }
    }
}