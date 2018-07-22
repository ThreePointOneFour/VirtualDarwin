using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_script : MonoBehaviour
{

    public string DNA;
    public double MutationProbability = 0.1;
    public double ChangeLengthProbability = 0.05;
    
    private int geneLength = 3;

    public string Mutate(string DNA)
    {
        //ChangeLength
        if(Random.value <= ChangeLengthProbability)
        {
            int length = (int)Random.Range(1, 3);
            //Lengthen
            if(Random.value < 0.5)
            {
                for(int i=0; i<length; i++) 
                    DNA = DNA + getRandomBase();
            }
            //Shorten
            else
            {
                DNA = DNA.Remove(DNA.Length -length, length);
            }

        }

        //Mutate
        for (int i = 0; i < DNA.Length; i++)
        {
            if (Random.value <= MutationProbability)
            {
                char change = getRandomBase();
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

    public void SetRandomDNA(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            DNA += getRandomBase();
        }
    }

    private char getRandomBase()
    {
        return ((int)Random.Range(0, 9)).ToString().ToCharArray()[0];
    }

    public int GetBaseAt(int at)
    {
        int b = (int)char.GetNumericValue(DNA[at]);
        return b;
    }

    public int getGeneLenght()
    {
        return geneLength;
    }
}
