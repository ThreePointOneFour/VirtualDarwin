using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class DNAO {

    public string DNA { get; private set; }

    private static readonly IDictionary<string, int> geneInfo = new Dictionary<string, int>()
    {
        { "type", 1 },
        { "dir", 1 },
    };

    private static readonly string[] CellType2Name = new string[]{ "CoreCell", "BoosterCell", "BaseCell" };

    public DNAO(string DNA = "")
    {
        if (DNA == "")
        {
            DNA = GetRandomDNA(1);
        }
        this.DNA = DNA;
    }

    public static int GetGeneLength()
    {
        int geneLength = 0;
        foreach (int length in geneInfo.Values)
        {
            geneLength += length;
        }
        return geneLength;
    }

    public static string GetCellById(int id)
    {
        if (id > CellType2Name.Length)
            id = CellType2Name.Length - 1;
        return CellType2Name[id];
    }

    public string Mutate(float MutateProb)
    {
        string newDNA = MutateLength(DNA, MutateProb);
        return MutateContent(newDNA, MutateProb);
    }

    private string MutateLength(string DNA, float prob)
    {
        if (Random.value <= prob)
        {
            int length = Random.Range(1, GetGeneLength());
            //Lengthen
            if (Random.value < 0.5)
            {
                for (int i = 0; i < length; i++)
                    DNA = DNA + GetRandomBase();
            }
            //Shorten
            else
            {
                DNA = DNA.Remove(DNA.Length - length, length);
            }

        }
        return DNA;
    }

    private string MutateContent(string DNA, float prob)
    {
        for (int i = 0; i < DNA.Length; i++)
        {
            if (Random.value <= prob)
            {
                char change = GetRandomBase();
                DNA = Utils.ChangeStrAt(DNA, change, i);
            }
        }
        return DNA;
    }

    public string GetGene(int nmb)
    {
        int geneLength = GetGeneLength();
        int start = (nmb - 1) * geneLength;
        return DNA.Substring(start, geneLength);
    }

    public int GetBase(int at)
    {
        int b = (int)char.GetNumericValue(DNA[at]);
        return b;
    }

    private static char GetRandomBase()
    {
        return ((int)Random.Range(0, 9)).ToString().ToCharArray()[0];
    }

    private static string GetRandomGene()
    {
        string gene = "";
        int geneLen = GetGeneLength();
        for(int i = 0; i < geneLen; i++)
        {
            gene += GetRandomBase();
        }
        return gene;
    }

    public static string GetRandomDNA(int geneCnt)
    {
        string DNA = "";
        for (int i = 0; i < geneCnt; i++)
        {
            DNA += GetRandomGene();
        }
        return DNA;
    }

    public List<IDictionary<string, int>> ParseDNA()
    {
        int geneCnt = GetGeneCnt();

        List<IDictionary<string, int>> ret = new List<IDictionary<string, int>>();

        for (int i = 0; i < geneCnt; i++) {
            
            string gene = GetGene(i);

            IDictionary<string, int> dic = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> e in geneInfo)
            {
                int value = GetInfo(gene, e.Key);
                dic.Add(e.Key, value);
            }
            ret.Add(dic);
        }
        return ret;
    }

    private int GetInfo(string gene, string info)
    {
        int strpnt = 0;
        foreach (KeyValuePair<string, int> e in geneInfo)
        {
            int len = e.Value;
            if (e.Key == info)
                return Utils.Str2int(gene.Substring(strpnt, len));
            strpnt += len;
        }
        return 0;
    }

    public int GetGeneCnt()
    {
        return (DNA.Length / GetGeneLength()) - (DNA.Length % GetGeneLength());
    }


    public int CalcBirthReq(PrefabLoaderWrapper_script pl)
    {
        int ret = 0;
        List<IDictionary<string, int>> parsedDNA = ParseDNA();

        foreach(IDictionary<string, int> e in parsedDNA)
        {
            string type = CellType2Name[e["type"]];

            GameObject cell = pl.Load(type, "/cells");
            if (cell == null) continue;
            ret += cell.GetComponent<Cell_script>().nutVal;
        }
        return ret;
    }
}
