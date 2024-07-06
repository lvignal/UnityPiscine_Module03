using UnityEngine;

namespace Module03.Turret
{
    public class BulletController : MonoBehaviour
    {
        public float TurretDamage;

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}