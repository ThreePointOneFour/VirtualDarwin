using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarCell_script : FoodCreatorCell_script {

    public float scaleBase = 1.3f;
    public int maxScale = 10;
    public float min = 0;

    private float scalor = 0;
    private Rigidbody2D rig2D;

    public override void Start()
    {
        base.Start();
        rig2D = GetComponent<Rigidbody2D>();
    }


    public override int CreateFood()
    {
        if (IsStill())
            scalor++;
        else
            scalor = 0;

        float pow = Mathf.Min(scalor, maxScale);

        float food = Mathf.Pow(scaleBase, pow) + (min - 1);

        return Mathf.RoundToInt(food);
    }

    private bool IsStill() {
        return rig2D.velocity.magnitude > 0.1;
    }
}
