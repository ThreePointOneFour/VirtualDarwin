using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Script : MonoBehaviour {

    private Cell_script Cell_script;

    public bool immortal = false;

    private void Start()
    {
        Cell_script = GetComponent<Cell_script>();
    }

    void OnDestroy()
    {
        print(gameObject + "died");
        Object Food = Resources.Load("Food_prefab");
        GameObject remains = Instantiate(Food, transform.position, Quaternion.identity) as GameObject;
        remains.GetComponent<eatable_script>().nutritionValue = Cell_script.nutVal;
    }

    void Update()
    {
        if (immortal) return;
        if (!IsConnected())
            Destroy(gameObject);
    }

    public bool IsConnected()
    {
        GameObject core = Cell_script.SearchCore();
        if (core == null)
            return false;
        else
            return true;

    }
}
