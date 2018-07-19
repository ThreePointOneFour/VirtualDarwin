using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_script : MonoBehaviour {

    public float boostStrength = 10;

    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

	void Update () {
        Vector2 boost = transform.up * boostStrength;
        rig.AddForce(boost);
	}
}
