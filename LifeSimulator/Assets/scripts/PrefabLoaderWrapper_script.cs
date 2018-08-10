using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class PrefabLoaderWrapper_script : MonoBehaviour {

    private PrefabLoader pl;
    void Start()
    {
        pl = new PrefabLoader();
    }

    public static PrefabLoaderWrapper_script GetPL()
    {
        return GameObject.Find("PrefabLoader").GetComponent<PrefabLoaderWrapper_script>();
    }

    public GameObject Load(string prefabName, string path="")
    {
        return pl.Load(prefabName, path);
    }
}
