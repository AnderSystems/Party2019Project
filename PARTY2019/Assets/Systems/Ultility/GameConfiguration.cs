using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigurations", menuName = "GameSettings/MainGameConfigurations")]
public class GameConfiguration : ScriptableObject
{
    [SerializeField]
    GameConfiguration settings;
    public static GameConfiguration Settings;

    public enum GameLanguage
    {
        Auto, Português, English, Espanol
    }
    [System.Serializable]
    public class _Game
    {
        public GameLanguage Language;
    }
    [SerializeField]
    public _Game Game;

    public GameLanguage GetLanguage()
    {
        GameLanguage LanguageValue = new GameLanguage();

        if(Game.Language == GameLanguage.Auto)
        {
            if(Application.systemLanguage == SystemLanguage.English)
            {
                LanguageValue = GameLanguage.English;
            }

            if (Application.systemLanguage == SystemLanguage.Spanish)
            {
                LanguageValue = GameLanguage.Espanol;
            }

            if (Application.systemLanguage == SystemLanguage.Portuguese)
            {
                LanguageValue = GameLanguage.Português;
            }
        } else
        {
            LanguageValue = Game.Language;
        }
        return LanguageValue;
    }

    public void Awake()
    {
        if (settings)
        {
            if(Settings != null && Settings != this)
            {
                Settings.settings = this;
                Settings = this;
            }
        }
    }
}
