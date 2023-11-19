using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = 3;
            slider.wholeNumbers = true;
        }

        private void Update()
        {
            slider.value = Player.Instance.GetHealth();
        }
    }
}