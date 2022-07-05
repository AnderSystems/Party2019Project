using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMenu : MonoBehaviour
{
    public bool UpdateButtons;
    public SimpleButton[] Buttons;
    public int ActivateButton;

    public void Awake()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].Menu = this;
            Buttons[i].Index = i;
            if (i != ActivateButton)
            {
                Buttons[i].MouseOver(false);
            }
        }
    }

    public void ChangeButton(int ButtonIndex)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].MouseOver(false);
        }
        ActivateButton = ButtonIndex;
    }
}
