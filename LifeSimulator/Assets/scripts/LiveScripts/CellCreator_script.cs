using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCreator_script : MonoBehaviour {

    public int radius;
    public int amount;
    public int DNALength;

    private Object Seed;

    // Use this for initialization
    void Start()
    {
        Seed = Resources.Load("Seed_prefab");

        CreateCells();
    }

    private void CreateCells()
    {
        Vector2 me2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos;

        for (int i = 0; i < amount; i++)
        {
            pos = me2D + Random.insideUnitCircle * radius;
            GameObject seed = Instantiate(Seed, pos, Quaternion.identity) as GameObject;
            DNA_script ds = seed.GetComponent<DNA_script>();
            ds.SetRandomDNA(DNALength);
        }
    }
}
