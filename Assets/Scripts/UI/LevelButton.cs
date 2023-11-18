using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string newText)
        {
            text.text = newText;
        }
    }
}