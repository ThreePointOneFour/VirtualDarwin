using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDNA_script : MonoBehaviour {

    private DNA_script DNA_script;
    private string DNA;

    private static readonly int MatrixSize = 3;
    private string[,] cMatrix = new string[MatrixSize, MatrixSize];

    private void Start()
    {
        DNA_script = GetComponent<DNA_script>();
        DNA = DNA_script.DNA;

        FillcMatrix();
        Paint();
    }

    private void FillcMatrix()
    {
        float genesPSQ = (DNA_script.DNA.Length / DNA_script.GetGeneLenght()) / MatrixSize^2;
        int genesPSQ_gened = Mathf.FloorToInt(genesPSQ);
        float progress = 0;
        int progress_gened = 0;

        for (int i = 0; i < cMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cMatrix.GetLength(1); j++)
            {
                string s = DNA.Substring(progress_gened, genesPSQ_gened);
                progress += genesPSQ;
                cMatrix[i, j] = s;

                if (progress <= genesPSQ_gened)
                    progress_gened += genesPSQ_gened;

                print(s);
            }
        }
    }

    private void GetColor()
    {

    }

    private void Paint()
    {

    }
}
