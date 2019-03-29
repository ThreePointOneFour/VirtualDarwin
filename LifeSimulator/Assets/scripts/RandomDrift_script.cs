using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class RandomDrift_script : MonoBehaviour {

    public int speed;

    public Rigidbody2D rig;

    private Looper Looper;

    private void Start()
    {
        Looper = new Looper(5.0f, true, RandDrift);
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
