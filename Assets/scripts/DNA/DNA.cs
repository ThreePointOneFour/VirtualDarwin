using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAUtils;
using System.Linq;

public class DNA
{
    public static float mutationChance = 0.2f;
    public static float GeneDoubleChance = 0.1f;
    private List<Gene> Genes;

    public DNA(List<Gene> Genes, int additionalGenes = 0)
    {
        this.Genes = Genes;

        for (int i = 0; i < additionalGenes; i++) {
            Genes.Add(new Gene());
        }
    }

    public DNA(Gene[] Genes) {
        this.Genes = new List<Gene>(Genes);
    }

    public DNA(int geneAmount = 1) {
        Genes = new List<Gene>();
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

    public DNA PerfectDuplicate() {
        List<Gene> newGenes = new List<Gene>();
        List<Gene> Genes = GetGenes();

        foreach (Gene gene in Genes)
        {
            Gene newGene = gene.Clone(0);
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

            if (type == "None") continue;

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

    public float ComparePercent(DNA CompareTo)
    {

        List<Gene> thisGenes = GetGenes();
        List<Gene> compareGenes = CompareTo.GetGenes();

        int minLength = Mathf.Min(GetLength(), CompareTo.GetLength());
        int maxLength = Mathf.Max(GetLength(), CompareTo.GetLength());

        int matches = 0;
        for (int i = 0; i < minLength; i++)
        {
            if (thisGenes[i].Equals(compareGenes[i]))
                matches++;
        }
        return matches / maxLength;
    }

    public KeyValuePair<CellType, int>[] GetTypeFrequency() {
        SortedDictionary<CellType, int> TypeFrequencyList = new SortedDictionary<CellType, int>();
        List<Gene> Genes = GetGenes();

        foreach (Gene gene in Genes) {
            CellType type = gene.GetCellType();
            if (type == CellType.None) continue;
            if (TypeFrequencyList.ContainsKey(type))
                TypeFrequencyList[type] = (int)TypeFrequencyList[type] + 1;
            else
                TypeFrequencyList.Add(type, 1);
        }


        KeyValuePair<CellType, int>[] ret = new KeyValuePair<CellType, int>[TypeFrequencyList.Count];
        int cnt = 0;
        foreach (KeyValuePair<CellType, int> kv in TypeFrequencyList.OrderByDescending(key => key.Value)) {
            ret[cnt] = new KeyValuePair<CellType, int>(kv.Key, kv.Value);
            cnt++;
        }

        return ret;
    }
}
