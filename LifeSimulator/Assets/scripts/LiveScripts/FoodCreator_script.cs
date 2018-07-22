using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreator_script : MonoBehaviour {

    public int radius;
    public int density;
    public float regrowLoop;
    public int regrowAmount;

    private float timer; 
    private Object Food;

	// Use this for initialization
	void Start () {
        Food = Resources.Load("Food_prefab");

        CreateFood((int)(2 * Mathf.PI * radius) * density);
	}

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= regrowLoop)
        {
            timer = 0;
            CreateFood(regrowAmount);
        }
    }

    private void CreateFood(int amount)
    {

        Vector2 me2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos;

        for(int i=0; i < amount; i++)
        {

            pos = me2D + Random.insideUnitCircle * radius;
            Instantiate(Food, pos ,Quaternion.identity);
        }
    }
        


}
