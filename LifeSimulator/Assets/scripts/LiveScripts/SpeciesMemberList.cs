using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class SpeciesMemberList {

    public float SimilarityGame = 0.95f;

    private List<GameObject> MemberList;
    private readonly string SpeciesName;


    public SpeciesMemberList(GameObject FirstMember, string name = null) {
        if (name == null)
            name = AutoCreateName();

        MemberList = new List<GameObject>();
        MemberList.Add(FirstMember);

    }

    private string AutoCreateName() {

        return RandomU.RandomString(2) + Random.Range(0, 9) + Random.Range(0, 9);
    }

    public bool TryAdd(GameObject contender) {
        foreach (GameObject member in MemberList) {
            if (Compare(member, contender) < SimilarityGame)
                return false;
        }
        MemberList.Add(contender);
        return true;
    }

    private float Compare(GameObject orga1, GameObject orga2) {
        DNA DNA1 = orga1.GetComponent<Organism_script>().GetDNA();
        DNA DNA2 = orga1.GetComponent<Organism_script>().GetDNA();

        return DNA1.ComparePercent(DNA2);
    }

    public bool Contains(GameObject item) {
        return MemberList.Contains(item);
    }

    public bool Remove(GameObject item) {
        return MemberList.Remove(item);
    }

    public string GetSpeciesName() {
        return SpeciesName;
    }

    public List<GameObject> GetMembers() {
        return MemberList;
    }
}
