using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDNA_script : MonoBehaviour {

    private DNA_script DNA_script;
    private GameObject dCube;

    private int geneLength;
    private static readonly int MatrixSize = 3;
    private string[,] cMatrix = new string[MatrixSize, MatrixSize];

    private void Start()
    {
        DNA_script = transform.parent.gameObject.GetComponent<DNA_script>();
        geneLength = DNA_script.GetGeneLenght();
        dCube = Resources.Load("DisplayCube_prefab") as GameObject;

        FillcMatrix();
        Paint();
    }

    private void FillcMatrix()
    {
        string DNA = DNA_script.DNA;
        int geneLength = DNA_script.GetGeneLenght();
        float progress = 0;
        float step = (DNA.Length / geneLength) / (Mathf.Pow(MatrixSize,2));

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

    private int GetInfo(string gene, ref int strpnt, string info)
    {
        int length = DNA_script.geneInfo[info];
        string temp = gene.Substring(strpnt, length);
        strpnt = strpnt + length;
        return Convert.ToInt32(temp);
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
