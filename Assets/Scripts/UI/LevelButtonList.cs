using UnityEngine;

namespace UI
{
    public class LevelButtonList : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            foreach (LevelSo level in gameManager.GetLevels())
            {
                LevelButton button = Instantiate(buttonPrefab, transform).GetComponent<LevelButton>();
                button.SetText(level.levelName);
            }
        }
    }
}