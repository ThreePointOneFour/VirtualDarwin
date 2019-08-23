using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAUtils;
using KitchenSink;
using System.Linq;

public class Color_script : MonoBehaviour
{

    public SpriteRenderer SpriteRenderer;
    public Cell_script Cell_Script;

    void Start()
    {
        SpriteRenderer.color = CalcColor();
    }

    private void Update()
    {
    }

    public Color CalcColor()
    {
        DNA DNA = Cell_Script.GetDNA();
        KeyValuePair<CellType, int>[] TypeFrequency = DNA.GetTypeFrequency();
        int CellTypeCnt = BaseU.GetEnumLength(typeof(CellType));

        int rV = (int)TypeFrequency[0].Key;

        int gV;
        if (TypeFrequency.Length < 2)
            gV = 0;
        else
            gV = (int)TypeFrequency[1].Key;

        int bV;
        if (TypeFrequency.Length < 3)
            bV = 0;
        else
            bV = (int)TypeFrequency[2].Key;

       

        float r = (float)rV / CellTypeCnt;
        float g = (float)gV / CellTypeCnt;
        float b = (float)bV / CellTypeCnt;

        Color ret = new Color(r, g, b);
        return ret;
    }
}
