using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class Cleaner_script : MonoBehaviour {

    public float loop = 5.0f;

    private Looper CleanLooper;

    void Start()
    {
        CleanLooper = new Looper(loop, true, CleanEmpty);
    }

    // Update is called once per frame
    void Update() {
        CleanLooper.Loop(Time.unscaledDeltaTime);

	}

    private void CleanEmpty()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach(GameObject go in allObjects)
        {
            if (go.transform.childCount > 0) continue;

            Component[] allComponents = go.GetComponents<Component>();
            if (allComponents.Length > 1) continue;
            
            Destroy(go);
        }
    }
}
