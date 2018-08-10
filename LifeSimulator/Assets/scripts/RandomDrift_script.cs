using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class RandomDrift_script : MonoBehaviour {

    public int speed = 3;

    private float timer;
    private Rigidbody2D rig;

    private Looper Looper;

    private void Start()
    {
        Looper = new Looper(RandDrift, 5.0f);

        rig = GetComponent<Rigidbody2D>();
    }

    void Update () {
        Looper.Loop(Time.deltaTime);
	}

    private void RandDrift()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        rig.AddForce(dir * speed);
    }
}
