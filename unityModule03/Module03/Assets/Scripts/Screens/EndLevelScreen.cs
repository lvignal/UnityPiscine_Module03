using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Module03.Screens
{
    public class EndLevelScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _baseRemainingHP;
        [SerializeField] private TextMeshProUGUI _baseRemainingEnergy;
        [SerializeField] private TextMeshProUGUI _enemiesKilled;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private List<GameObject> _ranksHighlights;

        public Action OnButtonClicked;
        
        public void Initialize(bool hasWon, bool isLastLevel, int baseRemainingHP, int baseRemainingEnergy, int enemiesKilled)
        {
            _title.text = hasWon ? "Victory !" : "Defeat...";
            _baseRemainingHP.text = $"Base HP : {baseRemainingHP}";
            _baseRemainingEnergy.text = $"Base Energy : {baseRemainingEnergy}";
            _enemiesKilled.text = $"Enemies killed : {enemiesKilled}";
            
            if (isLastLevel)
                _buttonText.text = hasWon? "End game" : "Retry";
            else
                _buttonText.text = hasWon ? "Next level" : "Retry";
        }
        
        public void InvokeButtonClicked()
        {
            OnButtonClicked?.Invoke();
        }

        public void HighlightRank(int rankIndex)
        {
            if (rankIndex < 0 || rankIndex >= _ranksHighlights.Count)
                return;
            _ranksHighlights[rankIndex].SetActive(true);
        }
    }
}
