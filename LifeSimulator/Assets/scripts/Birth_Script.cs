using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birth_Script : MonoBehaviour {

    private CellLoader_script CellLoader_script;
    private DNA_script DNA_script;

    public int birthreq;

    // Use this for initialization
    void Start () {
        CellLoader_script = GameObject.Find("CellLoader").GetComponent<CellLoader_script>();
        DNA_script = GetComponent<DNA_script>();

        birthreq = CalcBirthReq();
    }

    public int CalcBirthReq()
    {
        int geneLenght = DNA_script.getGeneLenght();
        int birthreq = 0;

        for(int i = 0; i < DNA_script.DNA.Length - (DNA_script.getGeneLenght() -1); i= i + geneLenght)
        {
            int id = DNA_script.GetBaseAt(i);
            int nutVal = CellLoader_script.GetCellbyID(id).GetComponent<Cell_script>().nutVal;
            birthreq = +nutVal;
        }
        return birthreq;
    }

    public void Birth()
    {
        string newGene = DNA_script.Mutate(DNA_script.DNA);

        Object seedprefab = Resources.Load("Seed_prefab");
        Vector2 source = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos = source + Random.insideUnitCircle.normalized * 5;
        GameObject seed = Instantiate(seedprefab, pos, Quaternion.identity) as GameObject;
        seed.GetComponent<DNA_script>().DNA = newGene;
    }
}
