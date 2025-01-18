using System;
using UnityEngine;

namespace Plataformas2DU.Settings
{
    public static class SettingsController
    {
        private static readonly string musicVolumeNormalizedPref = "musicVolume";

        private static readonly string sfxVolumeNormalizedPref = "sfxVolume";

        private static readonly string playerHasImmunityPref = "playerHasImmunity";

        private const float defaultFloatValue = 0.5f;
        private const bool defaultBoolValue = false;

        public static float MusicVolume
        {
            get
            {
                if (PlayerPrefs.HasKey(musicVolumeNormalizedPref))
                {
                    return PlayerPrefs.GetFloat(musicVolumeNormalizedPref);
                }

                return defaultFloatValue;
            }
            set => PlayerPrefs.SetFloat(musicVolumeNormalizedPref, value);
        }

        public static float SFXVolume
        {
            get
            {
                if (PlayerPrefs.HasKey(sfxVolumeNormalizedPref))
                {
                    return PlayerPrefs.GetFloat(sfxVolumeNormalizedPref);
                }

                return defaultFloatValue;
            }
            set => PlayerPrefs.SetFloat(sfxVolumeNormalizedPref, value);
        }

        public static bool PlayerHasImmunity
        {
            get
            {
                if (PlayerPrefs.HasKey(playerHasImmunityPref))
                {
                    return Convert.ToBoolean(PlayerPrefs.GetInt(playerHasImmunityPref));
                }

                return defaultBoolValue;
            }
            set => PlayerPrefs.SetInt(playerHasImmunityPref, Convert.ToInt32(value));
        }

        public static void SaveSettings() => PlayerPrefs.Save();
    }
}
