using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public static float mutationChance = 0.2f;
    public static float GeneDoubleChance = 0.1f;
    private List<Gene> Genes;

    public DNA(List<Gene> Genes)
    {
        this.Genes = Genes;
    }

    public DNA(Gene[] Genes) {
        this.Genes = new List<Gene>(Genes);
    }

    public DNA(int geneAmount = 1) {
        for (int i = 0; i < geneAmount; i++)
        {
            Genes.Add(new Gene());
        }
    }

    public List<Gene> GetGenes() {
        return Genes;
    }

    public Gene GetGene(int nmb) {
        return Genes[nmb];
    }

    public int GetLength() {
        return Genes.Count;
    }

    public DNA Duplicate() {
        List<Gene> newGenes = new List<Gene>();
        List<Gene> Genes = GetGenes();
        float individualMutProb = mutationChance / GetLength();

        foreach (Gene gene in Genes)
        {
            Gene newGene = gene.Clone(individualMutProb);

            //Gene Duplication
            if (Random.value < GeneDoubleChance)
                newGenes.Add(newGene);

            newGenes.Add(newGene);
        }
        return new DNA(newGenes);
    }

    public int GetBirthCost() {
        PrefabLoaderWrapper_script pl = PrefabLoaderWrapper_script.GetPL();
        List<Gene> Genes = GetGenes();

        int sum = 0;
        foreach (Gene gene in Genes)
        {
            string type = gene.GetCellType().ToString();

            GameObject cell = pl.Load(type, "cells");
            if (cell == null) continue;
            sum += cell.GetComponent<Cell_script>().GetStartCost();
        }
        return sum;
    }

    public override string ToString() {
        string str = "";
        List<Gene> Genes = GetGenes();
        foreach (Gene gene in Genes)
        {
            str += "" + gene.ToNumber();
        }

        return str;
    }
}
