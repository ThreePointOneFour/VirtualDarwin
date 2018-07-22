using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_script : MonoBehaviour {

    public int startFood = 10;

    private readonly float fcintervall = 1.0f;
    private readonly int overfood = 15;
    private Birth_Script Birth_Script;


    private int food;
    private float fctimer;

    // Use this for initialization
    void Awake () {
        food = startFood;
        Birth_Script = GetComponent<Birth_Script>();

	}
	
	// Update is called once per frame
	void Update () {
        FoodClock();

        CheckBirth();
        CheckDead();
	}

    public void Feed(int amount)
    {
        food = food + amount;
    }

    public void ReduceFood(int amount)
    {
        food = food - amount;
        if (food < 0)
            food = 0;
    }

    private void CheckBirth()
    {
        int birthreq = Birth_Script.birthreq;
        if (food >= birthreq + overfood)
        {
            Birth_Script.Birth();
            ReduceFood(birthreq);
        }
    }

    private void CheckDead()
    {
        if (food <= 0)
            Destroy(gameObject);
    }

    private void FoodClock()
    {
        fctimer += Time.deltaTime;

        if(fctimer >= fcintervall)
        {
            fctimer = 0;
            ReduceFood(1);
        }
    }
}