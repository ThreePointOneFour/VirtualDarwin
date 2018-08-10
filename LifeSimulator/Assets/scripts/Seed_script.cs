using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seed_script : MonoBehaviour {

    private readonly int MAX_SIZE = 10;

    private DNA_script DNA_script;
    private CellLoader_script CellLoader_script;

    private int geneLength;

    private Point[] dirs = new Point[] {
        new Point(0,0), //Mid
        new Point(1,0), //Right
        new Point(0,-1),//Down
        new Point(-1,0),//Left
        new Point(0,1), //Up
    };

    // Use this for initialization
    void Start () {
        DNA_script = GetComponent<DNA_script>();
        CellLoader_script = GameObject.Find("CellLoader").GetComponent<CellLoader_script>();
        geneLength = DNA_script.GetGeneLenght();

        BuildOrga(DNA_script.DNA);

        Destroy(this.gameObject);
    }

    private void BuildOrga(string DNA)
    {
        int[,] CellMatrix = BuildCellMatrix(MAX_SIZE);

        CellMatrix = PopulateCellMatrix(DNA, CellMatrix);

        GameObject CellBox = new GameObject("Organism_" + DNA);

        Vector2 mid = GetMid(CellMatrix).ToVector2();
        for (int i=0; i < CellMatrix.GetLength(0); i++)
        {
            for(int j=0; j < CellMatrix.GetLength(1); j++)
            {
                int type = CellMatrix[i, j];
                CreateCell(mid, type, i, j, CellBox);
            }
        }
    }

    private int [,] BuildCellMatrix(int xsize, int ysize = -1)
    {
        if (ysize == -1)
            ysize = xsize;
        int[,] CellMatrix;
        CellMatrix = new int[xsize, ysize];
        for (int i = 0; i < CellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < CellMatrix.GetLength(1); j++)
            {
                CellMatrix[i, j] = -1;
            }
        }
        return CellMatrix;
    }

    private int[,] PopulateCellMatrix(string DNA, int[,] CellMatrix)
    {
        Point pos = GetMid(CellMatrix);
        int pastDir = 1;
        for (int i = 0; i < DNA.Length - (geneLength - 1); i = i + geneLength)
        {
            string gene = DNA.Substring(i, geneLength);
            int strpnt = 0;

            int tlen = DNA_script.geneInfo["type"];
            int type = DNAUtils.Str2int(gene.Substring(strpnt,tlen));
            strpnt += tlen;
            int dlen = DNA_script.geneInfo["dir"];
            int dir = DNAUtils.Str2int(gene.Substring(strpnt, dlen));
            strpnt += dlen;

            dir = Mathf.FloorToInt(dir / 2);
            int index;
            if (dir == 0)
                index = dir;
            else
            {
                index = (dir + pastDir - 1);
                if (index > 4)
                    index = 1;
                pastDir = index;
            }
            pos.x += dirs[index].x;
            pos.y += dirs[index].y;

            CellMatrix[pos.x, pos.y] = type;
        }
        return CellMatrix;
    }

    private void CreateCell(Vector2 origin, int type, int x, int y, GameObject CellBox)
    {
        if (type == -1)
            return;
        Object cell = CellLoader_script.GetCellbyID(type);
        if (cell == null) return;

        Vector2 pos = new Vector2(this.transform.position.x + x - origin.x, this.transform.position.y + y - origin.y);
        GameObject go = Instantiate(cell, pos, Quaternion.identity, CellBox.transform) as GameObject;
        go.GetComponent<DNA_script>().DNA = this.DNA_script.DNA;
    }

    private Point GetMid(int[,] CellMatrix)
    {
        return new Point((CellMatrix.GetLength(0) / 2), (CellMatrix.GetLength(1) / 2));
    }
}
