using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner_script : MonoBehaviour {

    public float loop = 5.0f;

    private float timer = 0.0f;

    // Update is called once per frame
    void Update() {
        timer += Time.unscaledDeltaTime;
        if (timer >= loop) { 
            CleanEmpty();
            timer = 0;
        }
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
