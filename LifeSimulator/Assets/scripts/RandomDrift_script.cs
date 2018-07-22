using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrift_script : MonoBehaviour {

    public int speed = 3;
    public float changeInt = 5.0F;

    private float timer;
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update () {
        timer += Time.deltaTime;

        if(timer >= changeInt)
        {
            timer = 0;

            Vector2 dir = Random.insideUnitCircle.normalized;
            rig.AddForce(dir * speed);
        }
	}
}
