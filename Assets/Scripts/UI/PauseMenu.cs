using Plataformas2DU.GameCore;
using UnityEngine;

namespace Plataformas2DU.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private string mainMenuSceneName = "MainMenu";

        public void TogglePauseMenu()
        {
            if (gameObject.activeSelf)
            {
                GameManager.Instance.SetSettingsActive(false);
                Resume();
                return;
            }

            gameObject.SetActive(true);
            GameManager.Instance.SetFreezeGamePlay(true);
        }

        public void Resume()
        {
            GameManager.Instance.SetFreezeGamePlay(false);
            gameObject.SetActive(false);
        }

        public void ToggleSettingsMenu()
        {
            GameManager.Instance.ToggleSettings();
        }

        public void ReturnMainMenu()
        {
            GameManager.Instance.SetFreezeGamePlay(false);
            GameManager.Instance.LoadSceneAsync(mainMenuSceneName);
        }

        public void QuitGame()
        {
            GameManager.Instance.QuitGame();
        }
    }
}
