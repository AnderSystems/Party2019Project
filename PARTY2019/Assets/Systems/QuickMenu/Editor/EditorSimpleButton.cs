using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleButton))]
public class EditorSimpleButton : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleButton button = (SimpleButton)target;
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Navegation");

        return;
        //Up
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
        SimpleButton UpButton = new SimpleButton();
        button.UpButton = UpButton;
        UpButton = (SimpleButton)
        EditorGUILayout.ObjectField(button.UpButton, typeof(SimpleButton), true, GUILayout.MaxWidth(200));
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        //Horizontal
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
                    button.LeftButton = (SimpleButton)
                        EditorGUILayout.ObjectField(button.LeftButton, typeof(SimpleButton), true, GUILayout.MaxWidth(200));

            EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox(button.name.Replace("[BUTTON]", ""), MessageType.None, true);

            EditorGUILayout.BeginHorizontal();
                    button.RightButton = (SimpleButton)
                        EditorGUILayout.ObjectField(button.RightButton, typeof(SimpleButton), true, GUILayout.MaxWidth(200));
                    EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        //Down
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
                button.DownButton = (SimpleButton)
                    EditorGUILayout.ObjectField(button.DownButton, typeof(SimpleButton), true, GUILayout.MaxWidth(200));
            EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

    }

    void OnSceneGUI()
    {
        SimpleButton button = (SimpleButton)target;
        if (!button.Title)
        {
            if (button.GetComponent<TextMeshProUGUI>())
            {
                button.Title = button.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                button.Title = button.GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        button.UpdateTitle();
        button.name = "[BUTTON]" + button.Title.text;
    }
}
