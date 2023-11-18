using UnityEngine;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        private LevelSo level;

        public void RestartLevel()
        {
            GameManager.Instance.SetLevel(level);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            GameManager.Instance.MainMenu();
        }

        public void Continue()
        {
            GameManager.Instance.PauseGame(false);
        }

        public void SetLevel(LevelSo newLevel)
        {
            level = newLevel;
        }
    }
}