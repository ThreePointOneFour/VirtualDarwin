using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodCreatorCell_script : Cell_script {

    protected override void HalfSecondLoop()
    {
        base.HalfSecondLoop();
        Feed(CreateFood());
    }

    public abstract int CreateFood();
}
