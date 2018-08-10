using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;


public class Seed_script : Cell_script {

    private readonly int MAX_SIZE = 10;

    public PrefabLoaderWrapper_script pl;

    private Point[] dirs = new Point[] {
        new Point(0,0), //Mid
        new Point(1,0), //Right
        new Point(0,-1),//Down
        new Point(-1,0),//Left
        new Point(0,1), //Up
    };

    void Awake()
    {
        pl = PrefabLoaderWrapper_script.GetPL();
    }

    // Use this for initialization
    void Start () {

        BuildOrga();

        Destroy(gameObject);
    }

    private void BuildOrga()
    {
        int[,] CellMatrix = BuildCellMatrix(MAX_SIZE);

        CellMatrix = PopulateCellMatrix(CellMatrix);

        GameObject CellBox = new GameObject("Organism_" + DNAO.DNA);

        Vector2 mid = Utils.GetMatrixMid(CellMatrix).ToVector2();
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

    private int[,] PopulateCellMatrix(int[,] CellMatrix)
    {
        Point pos = Utils.GetMatrixMid(CellMatrix);
        int pastDir = 1;
        List<IDictionary<string, int>> parsedDNA = DNAO.ParseDNA();

        foreach (IDictionary<string, int> dic in parsedDNA)
        {
            int type = dic["type"];
            int dir = dic["dir"];

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
        Object cell = pl.Load(DNAO.GetCellById(type));
        if (cell == null) return;

        Vector2 pos = new Vector2(transform.position.x + x - origin.x, transform.position.y + y - origin.y);
        GameObject go = Instantiate(cell, pos, Quaternion.identity, CellBox.transform) as GameObject;
        go.GetComponent<Cell_script>().DNAO = new DNAO(DNAO.DNA);
    }
}
