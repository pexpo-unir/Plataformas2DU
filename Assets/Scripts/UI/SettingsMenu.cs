using Plataformas2DU.Player;
using Plataformas2DU.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Plataformas2DU.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public static SettingsMenu Instance { get; private set; }

        [SerializeField]
        private Slider musicVolumeSlider;

        [SerializeField]
        private Slider sfxVolumeSlider;

        [SerializeField]
        private Toggle hasImmunityToggle;

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

        private void OnDisable()
        {
            SettingsController.SaveSettings();
        }

        /// <summary>
        ///  Set the value of <see cref="musicVolumeSlider"/>.
        /// </summary>
        /// <param name="musicVolume">Normalized music value.</param>
        public void SetMusicVolumeValueSlider(float musicVolume)
        {
            musicVolumeSlider.value = musicVolume;
            SettingsController.MusicVolume = musicVolume;
        }

        /// <summary>
        /// Set the value of <see cref="sfxVolumeSlider"/>.
        /// </summary>
        /// <param name="sfxVolume">Normalized sound effects value.</param>
        public void SetSFXVolumeValueSlider(float sfxVolume)
        {
            sfxVolumeSlider.value = sfxVolume;
            SettingsController.SFXVolume = sfxVolume;
        }

        public void SetHasImmunityToggle(bool hasImmunity)
        {
            hasImmunityToggle.isOn = hasImmunity;
            SettingsController.PlayerHasImmunity = hasImmunity;

            var pc = FindFirstObjectByType<PlayerController>();
            if (pc)
            {
                pc.DamageComponent.HasImmunity = hasImmunity;
            }
        }
    }
}
