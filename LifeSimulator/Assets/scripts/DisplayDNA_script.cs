using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KitchenSink;

public class DisplayDNA_script : MonoBehaviour {
        
    private DNA DNAO;

    private static readonly int MatrixSize = 3;
    private List<string>[,] cMatrix = new List<string>[MatrixSize, MatrixSize];

    private void Start()
    {
        DNAO = transform.parent.gameObject.GetComponent<Cell_script>().DNAO;

        FillcMatrix();
        Paint();
    }

    private void FillcMatrix()
    {
        List<IDictionary<string, int>> parsedDNA = DNAO.ParseDNA();

        float step = (DNAO.genes.Length / DNA.GetGeneLength()) / (Mathf.Pow(MatrixSize, 2));

        foreach (IDictionary<string, int> dic in parsedDNA)
        {

        }

        float progress = 0;


        for (int i = 0; i < cMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cMatrix.GetLength(1); j++)
            {
                int start = Mathf.FloorToInt(progress);
                float next = (progress + step);
                int end = Mathf.RoundToInt(next);
                int length = Mathf.Max(end - start, 1);

                List<string> genes = DNAO.GetGenes(start, length);
                cMatrix[i, j] = genes;

                progress = next;
            }
        }
    }

    private Color GetColor(List<string> genes)
    {

        int amount = genes.Count;

        foreach(string gene in genes)
        {
            int type = DNA.GetInfo(gene, "type");
            int dir = DNA.GetInfo(gene, "dir");
            r += dir;
            g += type;
            b += (dir + type) / 2;
        }

        int typeOpt = MathU.PowOfTen(DNA.GetInfoLength("type"));
        int dirOpt = MathU.PowOfTen(DNA.GetInfoLength("dir"));

        r = r / amount * (255 / typeOpt);
        g = g / amount * (255 / dirOpt);
        b = b / amount * (255 / ((typeOpt + dirOpt) / 2));

        return new Color32((byte) r, (byte) g, (byte) b, 255);
    }

    private void Paint()
    {
        for (int i = 0; i < cMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cMatrix.GetLength(1); j++)
            {
                SetColor(GetColor(cMatrix[i, j]), i, j);
            }
        }
    }

    private void SetColor(Color c, int x, int y)
    {
        GameObject child = transform.Find(x + "-" + y).gameObject;
        Image img = child.GetComponent<Image>();
        img.color = c;
    }
}
