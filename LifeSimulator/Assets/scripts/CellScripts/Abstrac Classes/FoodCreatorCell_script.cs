using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodCreatorCell_script : Cell_script {

    public override void Awake()
    {
        base.Awake();
        HalfSecondLoop += CreateFoodLoop;
    }

    private void CreateFoodLoop() {
        Energy_script.Feed(CreateFood());
    }

    public abstract int CreateFood();
}
