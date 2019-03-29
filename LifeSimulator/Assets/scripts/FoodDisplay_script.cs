using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodDisplay_script : MonoBehaviour {

    public Energy_script Energy_script;
    public Text Text;

    public void Start()
    {
        Text = GetComponent<Text>();
    }

    void Update () {
        Text.text = "" + Energy_script.GetCurrentFood();    
	}
}
