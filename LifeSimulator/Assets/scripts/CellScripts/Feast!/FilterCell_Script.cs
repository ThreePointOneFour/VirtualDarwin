using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCell_Script : FoodCreatorCell_script {

    public float vel2Food = 1;

    private Rigidbody2D rig2D;

    public override void Start()
    {
        base.Start();
        rig2D = GetComponent<Rigidbody2D>();
    }

    public override int CreateFood() {

        return Mathf.RoundToInt(rig2D.velocity.magnitude * vel2Food);

    }
}
