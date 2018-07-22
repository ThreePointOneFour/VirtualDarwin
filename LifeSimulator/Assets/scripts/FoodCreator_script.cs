using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreator_script : MonoBehaviour {

    public int radius;
    public int density;

    private Object Food;

	// Use this for initialization
	void Start () {
        Food = Resources.Load("Food_prefab");

        CreateFood();
	}

    private void CreateFood()
    {
        int amount =(int) (2 * Mathf.PI * radius) * density;



        Vector2 pos;

        for(int i=0; i < amount; i++)
        {
            pos = Random.insideUnitCircle * radius;
            Instantiate(Food, pos ,Quaternion.identity);
        }
    }
        


}
