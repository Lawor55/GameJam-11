using System;
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

        private GameManager gameManager;

        private bool hasWon;
        private LevelSo level;


        private void Start()
        {
            gameManager = GameManager.Instance;

            nextButton.SetActive(hasWon);
            restartButton.SetActive(!hasWon);

            if (hasWon)
            {
                background.color = Color.green;
                headingText.text = "level completed";

                LevelSo[] levels = gameManager.GetLevels();
                int index = Array.IndexOf(levels, level);

                nextButton.SetActive(index + 1 <= levels.Length);
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
            gameManager.SetLevel(level);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            gameManager.MainMenu();
        }

        public void NextLevel()
        {
            LevelSo[] levels = gameManager.GetLevels();
            int index = Array.IndexOf(levels, level);
            gameManager.SetLevel(levels[index + 1]);
        }

        public void SetLevel(LevelSo newLevel)
        {
            level = newLevel;
        }
    }
}