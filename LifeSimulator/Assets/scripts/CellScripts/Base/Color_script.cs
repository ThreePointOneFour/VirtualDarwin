using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_script : MonoBehaviour
{

    public SpriteRenderer SpriteRenderer;
    public Cell_script Cell_Script;

    void Start()
    {
        SpriteRenderer.color = CalcColor();
    }

    public Color CalcColor()
    {
        int nmb = 0;
        DNA DNA = Cell_Script.GetDNA();
        string str = DNA.ToString();
        string hex = nmb.ToString("x");

        Color ret;
        ColorUtility.TryParseHtmlString(hex, out ret);
        return ret;
    }
}
