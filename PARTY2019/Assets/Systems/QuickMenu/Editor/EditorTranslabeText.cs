using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TranslabeText))]
public class EditorTranslabeText : Editor
{
    void OnSceneGUI()
    {
        TranslabeText text = (TranslabeText)target;
        if (!text.Title)
        {
            if (text.GetComponent<TextMeshProUGUI>())
            {
                text.Title = text.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                text.Title = text.GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        text.UpdateTitle();
        text.name = text.Title.text;
    }
}
