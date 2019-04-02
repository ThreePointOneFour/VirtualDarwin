using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class Organism_script : MonoBehaviour
{
    private Looper Looper;

    void Start()
    {
        Looper = new Looper(1.0f, true, CheckAlive);
    }

    void Update()
    {
        Looper.Loop(Time.deltaTime);
    }

    private void CheckAlive() {
        if (GetDNA() == null)
            Destroy(gameObject);
    }

    public DNA GetDNA() {
        Cell_script Cell_script = GetComponentInChildren<Cell_script>();
        if (Cell_script == null) return null;

        return Cell_script.GetDNA();
    }
}
