using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                GameObject button = Instantiate(buttonPrefab, transform);
                button.GetComponentInChildren<TMP_Text>().text = level.levelName;
                button.GetComponent<Button>().onClick.AddListener(() => { gameManager.SetLevel(level); });
            }
        }
    }
}