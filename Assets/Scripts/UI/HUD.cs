using UnityEngine;
using UnityEngine.UI;

namespace Plataformas2DU.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private Slider healthBarSlider;

        public void UpdateHealthBar(int oldValue, int newValue)
        {
            healthBarSlider.value = newValue;
        }
    }
}
