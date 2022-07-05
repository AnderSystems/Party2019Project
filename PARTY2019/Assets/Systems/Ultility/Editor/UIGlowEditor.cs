using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIGlow))]
public class UIGlowEditor : Editor
{
    public void OnSceneGUI()
    {
        UIGlow glow = (UIGlow)target;
        AddOutlines(glow);
        if(glow.Outlines.Count > 0)
        {

        }
    }

    public void AddOutlines(UIGlow target)
    {
        if (target.GlowResolution > 7)
        {
            Debug.LogError("Glow resolution is too large");
        }
        if (target.GlowResolution > 7)
            return;
        if (target.Outlines == null)
        {
            target.Outlines = new List<Outline>();
        }

        if (target.Outlines == null)
            return;

        for (int i = 0; i < target.Outlines.Count; i++)
        {
            if (target.Outlines.Count > target.GlowResolution ||
            target.Outlines[i] == null)
            {
                DestroyImmediate(target.Outlines[i]);
                if (target.Outlines[i] == null)
                {
                    target.Outlines.RemoveAt(i);
                }
            }
        }

        if (target.Outlines.Count < target.GlowResolution)
        {
            Outline _Outline = target.gameObject.AddComponent<Outline>();
            target.Outlines.Add(_Outline);
        }

        for (int i = 0; i < target.Outlines.Count; i++)
        {
            /*target.Outlines[i].effectColor = new Color(target.GlowColor.r / (i + 1), target.GlowColor.g /
                (i + 1), target.GlowColor.b / (i + 1),
                target.GlowColor.a / (i+1));*/
            float H;
            float S;
            float V;
            Color.RGBToHSV(target.GlowColor, out H, out S, out V);
            Color EffectColor = Color.HSVToRGB(H, (S*i) / (target.Outlines.Count/2), V);
            target.Outlines[i].effectColor = new Color(
                Color.HSVToRGB(H, (S * i) / (target.Outlines.Count * target.GlowColor.a), V).r
                , Color.HSVToRGB(H, (S * i) / (target.Outlines.Count * target.GlowColor.a), V).g
                , Color.HSVToRGB(H, (S * i) / (target.Outlines.Count * target.GlowColor.a), V).b,
                target.GlowColor.a / (i+1));
            target.Outlines[i].effectDistance = Vector2.one * target.GlowSize;
        }
    }
}
