using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class PrefabLoaderWrapper_script : MonoBehaviour {

    private PrefabLoader pl;


    public static PrefabLoaderWrapper_script GetPL()
    {
        return GameObject.Find("PrefabLoader").GetComponent<PrefabLoaderWrapper_script>();
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        pl = new PrefabLoader();
    }

    public GameObject Load(string prefabName, string path="")
    {
        if (pl == null)
            Init();
        return pl.Load(prefabName, path);
    }
}
