using Plataformas2DU.GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace Plataformas2DU.UI
{
    public class LevelsMenu : MonoBehaviour
    {
        [SerializeField]
        private Button[] levelButtons;

        private void Start()
        {
            gameObject.SetActive(false);

            bool unlocked;
            for (int i = 1; i < levelButtons.Length; i++)
            {
                unlocked = GameManager.Instance.IsLevelUnlocked(i + 1);
                levelButtons[i].interactable = unlocked;
            }
        }

        public void PlayLevel(int level)
        {
            GameManager.Instance.LoadSceneAsync($"Level{level}");
        }

        public void PlayLevelTest()
        {
            GameManager.Instance.LoadSceneAsync("LevelTest");
        }
    }
}
