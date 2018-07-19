using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_script : MonoBehaviour {

    private DNA_script DNA_script;

    private readonly int geneLength = 3;
    private readonly string[] cellType2Name = { "CoreCell", "BoosterCell","BlueCell", "RedCell", "GreenCell" };
    int[,] CellMatrix = new int[10, 10];

    // Use this for initialization
    void Start () {
        DNA_script = GetComponent<DNA_script>();

        BuildCell(DNA_script.DNA);

        Destroy(this.gameObject);
    }

    private void BuildCell(string DNA)
    {

        int type;
        int x;
        int y;

        for (int i = 0; i < CellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < CellMatrix.GetLength(1); j++)
            {
                CellMatrix[i, j] = -1;
            }
        }

        CellMatrix[(int)GetCmMid().x, (int)GetCmMid().y] = 0;

        for (int i = 0; i < DNA.Length - 2; i = i + geneLength)
        {
            string gene = DNA.Substring(i, geneLength);
            type = (int)char.GetNumericValue(gene[0]);
            x = (int)char.GetNumericValue(gene[1]);
            y = (int)char.GetNumericValue(gene[2]);

            CellMatrix[x, y] = type;
        }

        for (int i=0; i < CellMatrix.GetLength(0); i++)
        {
            for(int j=0; j < CellMatrix.GetLength(1); j++)
            {
                type = CellMatrix[i, j];
                CreateCell(CellMatrix[i, j], i, j);
            }
        }
    }

    private void CreateCell(int type, int x, int y)
    {
        if (type >= cellType2Name.Length) return;
        if (type == -1)
            return;
        Object cell = Resources.Load("cells/" + cellType2Name[type] + "_prefab");
        Vector2 pos = new Vector2(this.transform.position.x + x - GetCmMid().x, this.transform.position.y + y - GetCmMid().y);
        Instantiate(cell, pos, Quaternion.identity);
    }

    private Vector2 GetCmMid()
    {
        return new Vector2((CellMatrix.GetLength(0) / 2), (CellMatrix.GetLength(1) / 2));
    }
}
