using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCell_script : ConductorCell_script {


    public int boostStrength;
    public Rigidbody2D rig;

    public override void OnConductorLoop()
    {
        if (GetAttachedActiveCnt() > 0)
            Boost();
    }

    private void Boost()
    {
        Vector2 boost = transform.up * boostStrength;
        rig.AddForce(boost);
    }
}
