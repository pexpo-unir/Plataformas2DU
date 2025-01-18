using System;
using Plataformas2DU.Audio;
using Plataformas2DU.Settings;
using Plataformas2DU.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Plataformas2DU.GameCore
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private SettingsMenu settingsMenu;

        [SerializeField]
        private GameObject loadingScreen;

        [SerializeField]
        private Slider progressBar;

        private float fakeProgressValue = 0;

        [SerializeField]
        private float fakeProgressLoadSpeed = 0.75f;

        [SerializeField]
        private AudioController audioController;

        public bool IsGameplayPaused { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            settingsMenu.SetMusicVolumeValueSlider(SettingsController.MusicVolume);
            settingsMenu.SetSFXVolumeValueSlider(SettingsController.SFXVolume);
            settingsMenu.SetHasImmunityToggle(SettingsController.PlayerHasImmunity);

            loadingScreen.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
        }

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        public void LoadSceneAsync(string sceneName) => StartCoroutine(LoadSceneAsyncCoroutine(sceneName));

        private System.Collections.IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            loadingScreen.SetActive(true);

            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            asyncLoad.allowSceneActivation = false;
            audioController.ApplyLowpassFilter();

            while (!asyncLoad.isDone)
            {
                if (fakeProgressValue < 0.9f)
                {
                    fakeProgressValue += fakeProgressLoadSpeed * Time.deltaTime;
                }

                float progress = Mathf.Clamp01(Mathf.Min(fakeProgressValue, asyncLoad.progress / 0.9f));
                progressBar.value = progress;

                if (progress >= 0.9f && fakeProgressValue >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }

            audioController.RemoveLowpassFilter();
            loadingScreen.SetActive(false);
            fakeProgressValue = 0;
        }

        public void ReloadCurrentScene() => LoadScene(SceneManager.GetActiveScene().name);

        public void ToggleSettings() => SetSettingsActive(!settingsMenu.gameObject.activeSelf);

        public void SetSettingsActive(bool active) => settingsMenu.gameObject.SetActive(active);

        public void QuitGame() => Application.Quit();

        public bool IsLevelUnlocked(int level)
        {
            return Convert.ToBoolean(PlayerPrefs.GetInt($"Level{level}"));
        }

        public void UnlockLevel(int level)
        {
            PlayerPrefs.SetInt($"Level{level}", 1);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Applies <see cref="Time.timeScale"/> to 0 and
        /// LowpassFilter on <see cref="audioController"/>.
        /// </summary>
        /// <param name="value"></param>
        public void SetFreezeGamePlay(bool value)
        {
            if (value)
            {
                Time.timeScale = 0;
                audioController.ApplyLowpassFilter();
            }
            else
            {
                Time.timeScale = 1;
                audioController.RemoveLowpassFilter();
            }

            IsGameplayPaused = value;
        }
    }
}
