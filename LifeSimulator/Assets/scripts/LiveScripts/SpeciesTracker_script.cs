using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class SpeciesTracker_script : MonoBehaviour
{
    private Looper Looper;
    private List<GameObject> OldRecord;
    private List<SpeciesMemberList> SpeciesList = new List<SpeciesMemberList>();

    void Start()
    {
        Looper = new Looper(1.0f, true, TrackSpecies);
    }

    private void Update()
    {
        Looper.Loop(Time.deltaTime);
    }

    private void TrackSpecies() {
        List<GameObject> NewRecord = FetchOrganisms();

        AddNew(OldRecord, NewRecord);
        ClearOld(OldRecord, NewRecord);  
    }

    private void AddNew(List<GameObject> OldRecord, List<GameObject> NewRecord) {
        foreach (GameObject orga in NewRecord) {
            if (!OldRecord.Contains(orga))
                Add(orga);
        }
    }

    private void ClearOld(List<GameObject> OldRecord, List<GameObject> NewRecord) {
        foreach (GameObject orga in OldRecord) {
            if (!NewRecord.Contains(orga))
                Remove(orga);
        }

    }

    private List<GameObject> FetchOrganisms() {
        GameObject[] Found = GameObject.FindGameObjectsWithTag("Organism");
        return new List<GameObject>(Found);
    }

    private void Add(GameObject toAdd) {
        bool res = false;
        foreach (SpeciesMemberList speciesMemberList in SpeciesList) {
            res = speciesMemberList.TryAdd(toAdd);
            if (res) return;
        }
        if (!res) {
            SpeciesList.Add(new SpeciesMemberList(toAdd));
        }
    }

    private void Remove(GameObject toRemove) {
        foreach(SpeciesMemberList speciesMemberList in SpeciesList) {
            if (speciesMemberList.Remove(toRemove))
                return;
        }
    }

    public List<SpeciesMemberList> GetSpeciesList()
    {
        return SpeciesList;
    }

    public string GetSpecies(GameObject Organism)
    {
        foreach (SpeciesMemberList speciesMemberList in SpeciesList) {
            if (speciesMemberList.Contains(Organism))
                return speciesMemberList.GetSpeciesName();
        }
        return "none";
    }

    public List<GameObject> GetMembers(string Species) {
        foreach (SpeciesMemberList speciesMemberList in SpeciesList)
        {
            if (speciesMemberList.GetSpeciesName() == Species)
                return speciesMemberList.GetMembers();
        }
        return default;
    }

}
