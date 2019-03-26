using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public static float mutationChance = 0.2f;
    public static float GeneDoubleChance = 0.1f;
    private List<Gene> Genes;

    public DNA(List<Gene> Genes)
    {
        this.Genes = Genes;
    }

    public DNA(int geneAmount = 1) {
        for(int i = 0; i< geneAmount; i++)
        {
            Genes.Add(new Gene());
        }
    }

    public List<Gene> GetDNA() {
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

    public Color GetColor() {
        int nmb = 0;
        foreach (Gene gene in Genes)
        {
            nmb += gene.ToNumber();
        }
        string hex = nmb.ToString("x");

        Color ret;
        ColorUtility.TryParseHtmlString(hex, out ret);
        return ret;
    }
}
