using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_script : MonoBehaviour {

    private DNA_script DNA_script;
    private CellLoader_script CellLoader_script;

    private int geneLength;
    int[,] CellMatrix;

    // Use this for initialization
    void Start () {
        DNA_script = GetComponent<DNA_script>();
        CellLoader_script = GameObject.Find("CellLoader").GetComponent<CellLoader_script>();
        geneLength = DNA_script.GetGeneLenght();


        int xLength = 10^DNA_script.geneInfo["x"];
        int yLength = 10^DNA_script.geneInfo["y"];
        CellMatrix = new int[xLength, yLength ];

        BuildOrga(DNA_script.DNA);

        Destroy(this.gameObject);
    }

    private void BuildOrga(string DNA)
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


        for (int i = 0; i < DNA.Length - (geneLength -1); i = i + geneLength)
        {
            string gene = DNA.Substring(i, geneLength);
            type = (int)char.GetNumericValue(gene[0]);
            x = (int)char.GetNumericValue(gene[1]);
            y = (int)char.GetNumericValue(gene[2]);

            CellMatrix[x, y] = type;
        }

        GameObject CellBox = new GameObject("Organism_" + DNA);

        for (int i=0; i < CellMatrix.GetLength(0); i++)
        {
            for(int j=0; j < CellMatrix.GetLength(1); j++)
            {
                type = CellMatrix[i, j];
                CreateCell(CellMatrix[i, j], i, j, CellBox);
            }
        }
    }

    private void CreateCell(int type, int x, int y, GameObject CellBox)
    {
        if (type == -1)
            return;
        Object cell = CellLoader_script.GetCellbyID(type);
        if (cell == null) return;

        Vector2 pos = new Vector2(this.transform.position.x + x - GetCmMid().x, this.transform.position.y + y - GetCmMid().y);
        GameObject go = Instantiate(cell, pos, Quaternion.identity, CellBox.transform) as GameObject;
        go.GetComponent<DNA_script>().DNA = this.DNA_script.DNA;
    }

    private Vector2 GetCmMid()
    {
        return new Vector2((CellMatrix.GetLength(0) / 2), (CellMatrix.GetLength(1) / 2));
    }
}
