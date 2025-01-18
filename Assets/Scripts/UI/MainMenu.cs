using Plataformas2DU.GameCore;
using UnityEngine;

namespace Plataformas2DU.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject levelsMenu;

        public void ToggleLevels()
        {
            levelsMenu.SetActive(!levelsMenu.activeSelf);
        }

        public void ToggleSettings()
        {
            GameManager.Instance.ToggleSettings();
        }

        public void QuitGame()
        {
            GameManager.Instance.QuitGame();
        }
    }
}
