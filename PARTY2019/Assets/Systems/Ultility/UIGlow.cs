using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIGlow : MonoBehaviour
{
    public Graphic Target;
    [Space]
    public Color GlowColor = new Color(1,1,1,.5f);
    public bool UseGraphicAlpha = true;
    [Range(1,7)]
    public int GlowResolution = 4;
    public float GlowSize = 1;

    public List<Outline> Outlines { get; set; }
}
