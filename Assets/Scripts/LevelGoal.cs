using Plataformas2DU.GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plataformas2DU.Gameplay
{
    public class LevelGoal : MonoBehaviour
    {
        [SerializeField]
        private int nextLevel;

        [SerializeField]
        private string defaultScene = "MainMenu";

        [SerializeField]
        private float floatSpeed = 2f;

        [SerializeField]
        private float floatAmplitude = 1f;

        [SerializeField]
        private GameObject text;

        private Vector3 textInitialPosition;

        private void Start()
        {
            textInitialPosition = text.transform.position;
        }

        private void Update()
        {
            float newY = Mathf.Cos(Time.time * floatSpeed) * floatAmplitude;
            text.transform.position = new Vector3(textInitialPosition.x, textInitialPosition.y + newY, textInitialPosition.z);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            GameManager.Instance.UnlockLevel(nextLevel);
            if (SceneUtility.GetBuildIndexByScenePath($"Level{nextLevel}") != -1)
            {
                GameManager.Instance.LoadSceneAsync($"Level{nextLevel}");
            }
            else
            {
                GameManager.Instance.LoadSceneAsync(defaultScene);
            }
        }

    }
}
