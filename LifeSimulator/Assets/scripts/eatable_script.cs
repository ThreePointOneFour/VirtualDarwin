using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatable_script : MonoBehaviour {

    public int nutritionValue = 5;

    public int eat()
    {
        Destroy(gameObject);
        return nutritionValue;
    }

}
