using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDNA_script : MonoBehaviour {

    /*
    public DNAO DNAO;

    private static readonly int MatrixSize = 3;
    private string[,] cMatrix = new string[MatrixSize, MatrixSize];

    private void Start()
    {
        DNAO = transform.parent.gameObject.GetComponent<Cell_script>().DNAO;

        FillcMatrix();
        Paint();
    }

    private void FillcMatrix()
    {
        List<IDictionary<string, int>> parsedDNA = DNAO.ParseDNA();

        float step = (DNAO.DNA.Length / DNAO.GetGeneLength()) / (Mathf.Pow(MatrixSize, 2));

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
               
                string s = DNA.Substring(start * geneLength, length * geneLength);
                cMatrix[i, j] = s;

                progress = next;
            }
        }
    }

    private Color GetColor(string genes)
    {

        int r = 0;
        int g = 0;
        int b = 0;

        int cnt = 0;
        for(int i=0; i < genes.Length; i = i+ geneLength)
        {
            string gene = genes.Substring(i, geneLength);

            int strpnt = 0;
            r += GetInfo(gene, ref strpnt, "type");
            g += GetInfo(gene, ref strpnt, "type");
            b += 10; //GetInfo(gene, ref strpnt, "dir");
            cnt++;
        }
        r = r / cnt * (255 / 10);
        g = g / cnt * (255 / 10);
        b = b / cnt * (255 / 10);

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
    */
}
