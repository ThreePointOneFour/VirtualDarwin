using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelCell_script : FoodCreatorCell_script {

    public float scaleBase = 1.3f;
    public int maxScale = 10;
    public float min = 0;

    public Rigidbody2D rig2D;

    private float scalor = 0;


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
        return rig2D.velocity.magnitude < 0.01f;
    }
}
