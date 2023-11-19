using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private Sprite winSprite;
        [SerializeField] private Sprite gameOverSprite;
        [SerializeField] private TMP_Text headingText;
        [SerializeField] private Image background;

        private bool hasWon;
        private LevelSo level;


        private void Start()
        {
            nextButton.SetActive(hasWon);
            restartButton.SetActive(!hasWon);

            if (hasWon)
            {
                background.color = Color.green;
                headingText.text = "level completed";
            }
            else
            {
                background.color = Color.red;
                headingText.text = "game over";
            }
        }

        public void SetHasWon(bool hasWon)
        {
            this.hasWon = hasWon;
        }

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

        public void NextLevel()
        {
            GameManager.Instance.PauseGame(false);
        }

        public void SetLevel(LevelSo newLevel)
        {
            level = newLevel;
        }
    }
}