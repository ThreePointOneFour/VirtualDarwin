using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodDisplay_script : MonoBehaviour {

    public Cell_script CellScript;
    public Text Text;

    public void Start()
    {
        CellScript = GetComponentInParent<Cell_script>();
        Text = GetComponent<Text>();
    }

    void Update () {
        Text.text = "" + CellScript.GetCurrentFood();    
	}
}
