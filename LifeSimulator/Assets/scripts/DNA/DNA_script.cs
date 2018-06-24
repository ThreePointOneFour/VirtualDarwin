using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_script : MonoBehaviour
{

    public string DNA;
    public double MutationProbability;

    private readonly string[] cellType2Name = { "BlueCell", "RedCell", "GreenCell" };

    private readonly int geneLength = 3;

    // Use this for initialization
    void Start()
    {
        print("DNA" + DNA);
        print("Mutated" + Mutate(DNA));
    }

    private void ParseDNA(string DNA)
    {
        int ctype;
        int cposX;
        int cposY;

        for (int i = 0; i < DNA.Length - 2; i = i + geneLength)
        {
            string gene = DNA.Substring(i, geneLength);
            ctype = (int)char.GetNumericValue(gene[0]);
            cposX = (int)char.GetNumericValue(gene[1]);
            cposY = (int)char.GetNumericValue(gene[2]);
            CreateCell(ctype, cposX, cposY);
        }

    }

    public void GetDNA()
    {
    }
    public void BuildOrganism()
    {
        ParseDNA(DNA);
    }

    private int Char2Int(char c)
    {
        return (int)char.GetNumericValue(c);
    }

    private void CreateCell(int type, int x, int y)
    {
        Object cell = Resources.Load("cells/" + cellType2Name[type] + "_prefab");
        Vector2 pos = new Vector2(this.transform.position.x + x - 5, this.transform.position.y + y - 5);
        Instantiate(cell, pos, Quaternion.identity);
    }

    private string ChangeAt(string str, char change, int at)
    {
        string front = str.Substring(0, at);
        string back = str.Substring(at + 1, str.Length - (at + 1));
        return front + change + back;
    }

    private string Mutate(string DNA)
    {
        for (int i = 0; i < DNA.Length; i++)
        {
            if (Random.value <= MutationProbability)
            {
                char change = ((int)Random.Range(0, 9)).ToString().ToCharArray()[0];
                DNA = ChangeAt(DNA, change, i);
            }
        }
        return DNA;
    }
}
