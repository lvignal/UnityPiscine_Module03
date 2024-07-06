using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module03.Screens
{
    public class EndGameScreen : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}