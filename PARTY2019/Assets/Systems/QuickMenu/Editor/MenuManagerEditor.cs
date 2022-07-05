using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MenuManager))]
public class MenuManagerEditor : Editor
{
    void OnSceneGUI()
    {
        MenuManager menu = (MenuManager)target;

        for (int i = 0; i < menu.Menus.Length; i++)
        {
            menu.Menus[i].MenuName = "(" + i + ")" + menu.Menus[i].MenuObject.gameObject.name;
        }
    }
}
