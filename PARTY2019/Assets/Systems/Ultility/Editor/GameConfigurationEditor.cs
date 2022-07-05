using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameConfiguration))]
public class GameConfigurationEditor : Editor
{
    void OnSceneGUI()
    {
        GameConfiguration Settings = (GameConfiguration)target;

        Settings.Awake();
    }
}
