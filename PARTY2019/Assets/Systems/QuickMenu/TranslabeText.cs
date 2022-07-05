using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslabeText : MonoBehaviour
{
    public GameConfiguration Settings;
    public TextMeshProUGUI Title;
    [Space]
    [TextArea]
    public string PTBR_Title = "Botão";
    [TextArea]
    public string ENUS_Title = "Button";
    [TextArea]
    public string SPN_Title = "Butón";

    public void SetTitle(string PTBR, string ENUS, string SPN)
    {
        PTBR_Title = PTBR;
        ENUS_Title = ENUS;
        SPN_Title = SPN;
        UpdateTitle();
    }
    public void UpdateTitle()
    {
        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.English)
        {
            Title.text = ENUS_Title;
        }

        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.Espanol)
        {
            Title.text = SPN_Title;
        }

        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.Português)
        {
            Title.text = PTBR_Title;
        }
    }
}
