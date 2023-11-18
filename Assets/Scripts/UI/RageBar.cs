using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RageBar : MonoBehaviour
    {
        private GameManager gameManager;
        private Slider slider;

        private void Start()
        {
            gameManager = GameManager.Instance;
            slider = GetComponent<Slider>();

            slider.minValue = 0;
            slider.maxValue = gameManager.GetLevelMaxRage();
        }

        private void Update()
        {
            slider.value = gameManager.GetRageValue();
        }
    }
}