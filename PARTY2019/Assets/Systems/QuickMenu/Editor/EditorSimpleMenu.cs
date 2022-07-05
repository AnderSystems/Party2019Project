using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleMenu))]
public class EditorSimpleMenu : Editor
{

    void OnSceneGUI()
    {
        SimpleMenu menu = (SimpleMenu)target;
        if (menu.UpdateButtons)
        {
            menu.Buttons = menu.GetComponentsInChildren<SimpleButton>();
            for (int i = 0; i < menu.Buttons.Length; i++)
            {
                menu.Buttons[i].Menu = menu;
                menu.Buttons[i].Index = i;
            }
            if(EditorUtility.DisplayDialog("...", "Buttons Updated!", "ok"))
            menu.UpdateButtons = false;
        }
    }
}
