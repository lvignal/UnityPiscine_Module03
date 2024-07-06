using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module03.Screens
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _quitConfirmationPanel;
        
        public void Pause()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        
        public void OpenQuitConfirmation()
        {
            _quitConfirmationPanel.SetActive(true);
        }
        
        public void CloseQuitConfirmation()
        {
            _quitConfirmationPanel.SetActive(false);
        }

        public void GoBackToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}