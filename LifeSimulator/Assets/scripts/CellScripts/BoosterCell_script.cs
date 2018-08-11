using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCell_script : Cell_script {


    public int boostStrength;
    public Rigidbody2D rig;

    protected override void HalfSecondLoop()
    {
        Boost();
    }

    private void Boost()
    {
        Vector2 boost = transform.up * boostStrength;
        rig.AddForce(boost);
    }
}
