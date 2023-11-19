using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;


        private Button button;


        public void SetText(string newText)
        {
            text.text = newText;
        }
    }
}