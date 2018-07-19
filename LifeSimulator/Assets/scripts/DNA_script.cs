using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_script : MonoBehaviour
{

    public string DNA;
    public double MutationProbability;

    // Use this for initialization
    void Awake()
    {
        if (DNA == "")
            DNA = GetRandomDNA(10);
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

    private string ChangeAt(string str, char change, int at)
    {
        string front = str.Substring(0, at);
        string back = str.Substring(at + 1, str.Length - (at + 1));
        return front + change + back;
    }

    private string GetRandomDNA(int lenght)
    {
        string DNA = "";
        for (int i = 0; i < 10; i++)
        {
            DNA += ((int)Random.Range(0, 9)).ToString();
        }
        return DNA;
    }
}
