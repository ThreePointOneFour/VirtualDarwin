using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

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
        DNA DNA = Cell_Script.GetDNA();
        string DNAString = DNA.ToString();

        DNAString = DNAString.PadLeft(3, '0');
        string[] split = BaseU.StringSplit(DNAString, 3);

        float rF = MathU.Percentify(int.Parse(split[0]));
        float gF = MathU.Percentify(int.Parse(split[1]));
        float bF = MathU.Percentify(int.Parse(split[2]));

        int r = Mathf.RoundToInt(rF * 255);
        int g = Mathf.RoundToInt(gF * 255);
        int b = Mathf.RoundToInt(bF * 255);

        Color ret = new Color(r, g, b);
        return ret;
    }
}
