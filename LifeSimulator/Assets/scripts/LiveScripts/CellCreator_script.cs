using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAUtils;

public class CellCreator_script : MonoBehaviour {

    public int radius;
    public int SpeciesCount;
    public int MembersCount;

    public GeneEntry[] Base;
    public int extraGenes;

    private Object Seed;
    private PrefabLoaderWrapper_script pl;

    // Use this for initialization
    void Start()
    {
        pl = PrefabLoaderWrapper_script.GetPL();
        Seed = pl.Load("Seed");

        CreateOrganisms();
    }

    private void CreateOrganisms()
    {
        Vector2 me2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos;

        List<DNA> SeedDNA = CreateSeedDNA();

        foreach(DNA DNA in SeedDNA)
        {
            pos = (Vector2)transform.position + Random.insideUnitCircle * radius;
            GameObject seed = Instantiate(Seed, pos, Quaternion.identity) as GameObject;
            Seed_script Seed_script = seed.GetComponent<Seed_script>();
            Seed_script.SetDNA(DNA);
        }
    }

    private List<DNA> CreateSeedDNA() {
        List<DNA> SeedDNA = new List<DNA>();

        for (int i = 0; i < SpeciesCount; i++) {
            List<Gene> BaseL = DNAU.GeneEntries2GeneList(Base);
            DNA SpeciesDNA = new DNA(BaseL, extraGenes);
            for (int j = 0; j < MembersCount; j++) {
                SeedDNA.Add(SpeciesDNA.Duplicate());
            }
        }

        return SeedDNA;
    }
}
