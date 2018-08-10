using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellLoader_script : MonoBehaviour {

    private readonly string[] cellType2Name = { "CoreCell", "BoosterCell", "BaseCell"};
    private GameObject[] LoadedCells;

    void Start () {
        Load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetCellbyID(int id)
    {
        if (LoadedCells == null) Load();

        if (id >= LoadedCells.Length)
            id = LoadedCells.Length -1;

        return LoadedCells[id];
    }

    private void Load()
    {
        LoadedCells = new GameObject[cellType2Name.Length];
        for(int i=0; i < LoadedCells.Length; i++)
        {
            LoadedCells[i] = Resources.Load("cells/" + cellType2Name[i] + "_prefab") as GameObject;
        }
    }
}
