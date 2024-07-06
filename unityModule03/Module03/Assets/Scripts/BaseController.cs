using System;
using TMPro;
using UnityEngine;

namespace Module03.Base
{
    public class BaseController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _baseHPText;
        [SerializeField] private TextMeshProUGUI _baseEnergyText;
        [SerializeField] private int _healthPoints = 5;
        [SerializeField] private int _energyPoints = 80;
        
        public int HealthPoints => _healthPoints;
        
        public int EnergyPoints
        {
            get => _energyPoints;
            private set
            {
                _energyPoints = value;
                _baseEnergyText.text = "Energy : " + _energyPoints;
                OnEnergyPointsChanged?.Invoke(_energyPoints);
            }
        }

        public Action OnAllHPLost;
        public Action<int> OnEnergyPointsChanged;

        private void Start()
        {
            EnergyPoints = _energyPoints;
            _baseHPText.text = "HP : " + _healthPoints;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                _healthPoints--;
                _baseHPText.text = "HP : " + _healthPoints;
                Destroy(other.gameObject);
                Debug.Log("Now the base has " + _healthPoints + " HP");
            }

            if (_healthPoints <= 0)
                OnAllHPLost?.Invoke();
        }
        
        public void AddEnergy(int energy)
        {
            EnergyPoints += energy;
        }
        
        public void RemoveEnergy(int energy)
        {
            EnergyPoints -= energy;
        }
    }
}