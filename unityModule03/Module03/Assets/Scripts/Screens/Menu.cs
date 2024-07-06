using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module03.Screens
{
    public class Menu : MonoBehaviour
    {
        public void LaunchGame()
        {
            SceneManager.LoadScene(1);
        }
        
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}