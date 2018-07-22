using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_script : MonoBehaviour {

    public float boostStrength = 10;
    public float boostInt = 0.5f;

    private float boostTimer;

    private Rigidbody2D rig;

    void Start()
    {
        boostTimer = boostInt;
        rig = GetComponent<Rigidbody2D>();
    }

	void Update () {
        BoostLoop();
	}

    private void BoostLoop()
    {
        boostTimer += Time.deltaTime;
        if(boostTimer >= boostInt)
        {
            boostTimer = 0;
            Boost();
        }
    }

    private void Boost()
    {
        Vector2 boost = transform.up * boostStrength;
        rig.AddForce(boost);
    }
}
