using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_script : MonoBehaviour {

    public int nutritionValue = 5;

    public int Eat()
    {
        Destroy(gameObject);
        return nutritionValue;
    }

}
