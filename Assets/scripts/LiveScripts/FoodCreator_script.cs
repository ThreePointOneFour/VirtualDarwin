using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreator_script : MonoBehaviour {

    public int radius;
    public int density;
    public float regrowLoop;
    public int regrowAmount;

    private PrefabLoaderWrapper_script pl;

    private float timer; 
    private Object Food;

    private void Awake()
    {
        pl = PrefabLoaderWrapper_script.GetPL();
    }

    // Use this for initialization
    void Start () {
        Food = pl.Load("Food");

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
