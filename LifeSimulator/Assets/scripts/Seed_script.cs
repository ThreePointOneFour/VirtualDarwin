using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;
using DNAUtils;

public class Seed_script : MonoBehaviour {

    [System.Serializable]
    public class GeneEntry
    {
        public PhysicsU.Directions Direction;
        public CellType Type;
    }

    public readonly int MAX_SIZE = 10;

    public GeneEntry[] customDNA;
    private DNA DNA;
    private PrefabLoaderWrapper_script pl;

    void Awake()
    {
        pl = PrefabLoaderWrapper_script.GetPL();
        if (customDNA != null) {
            List<Gene> Genes = new List<Gene>();
            foreach (GeneEntry e in customDNA) {
                Genes.Add(new Gene(e.Direction, e.Type));
            }
            DNA = new DNA(Genes);
        }
    }

    public void SetDNA(DNA DNA) {
        this.DNA = DNA;
    }

    void Start () {

        Vector2 originalPos = transform.position;
        PhysicsU.Teleport(gameObject, Random.insideUnitCircle.normalized * 1000);

        GameObject Organism = BuildOrga();
        PhysicsU.Teleport(Organism, originalPos);

        Destroy(gameObject);
    }

    private GameObject BuildOrga()
    {
        CellType[,] CellMatrix = BuildCellMatrix(MAX_SIZE);
        PopulateCellMatrix(ref CellMatrix);

        GameObject Organism = new GameObject("Organism_" + DNA.ToString());

        Vector2 mid = BaseU.GetMatrixMid(CellMatrix).ToVector2();
        for (int i=0; i < CellMatrix.GetLength(0); i++)
        {
            for(int j=0; j < CellMatrix.GetLength(1); j++)
            {
                CellType type = CellMatrix[i, j];
                CreateCell(mid, type, i, j, Organism);
            }
        }
        return Organism;
    }

    private CellType[,] BuildCellMatrix(int xsize, int ysize = -1)
    {
        if (ysize == -1)
            ysize = xsize;
        CellType[,] CellMatrix;
        CellMatrix = new CellType[xsize, ysize];
        for (int i = 0; i < CellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < CellMatrix.GetLength(1); j++)
            {
                CellMatrix[i, j] = CellType.None;
            }
        }
        return CellMatrix;
    }

    private void PopulateCellMatrix(ref CellType[,] CellMatrix)
    {
        List<Gene> Genes = DNA.GetGenes();

        Point pos = BaseU.GetMatrixMid(CellMatrix);

        foreach (Gene gene in Genes)
        {
            CellType type = gene.GetCellType();
            PhysicsU.Directions dir = gene.GetDirection();
            Vector2 dirVec = PhysicsU.Dir2Vec(dir);

            pos.x += (int)dirVec.x;
            pos.y += (int)dirVec.y;

            pos.x = MathU.MinMax(pos.x, 0, CellMatrix.GetLength(0) -1);
            pos.y = MathU.MinMax(pos.y, 0, CellMatrix.GetLength(1) -1);

            CellMatrix[pos.x, pos.y] = type;
        }
    }

    private void CreateCell(Vector2 origin, CellType type, int x, int y, GameObject Organism)
    {
        if (type == CellType.None) return;

        GameObject cell = pl.Load(type.ToString(), "cells");
        if (cell == null) return;

        Vector2 pos = new Vector2(x - origin.x, y - origin.y);
        GameObject go = Instantiate(cell, pos, Quaternion.identity, Organism.transform) as GameObject;
        go.GetComponent<Cell_script>().SetDNA(DNA.Duplicate());
    }
}
