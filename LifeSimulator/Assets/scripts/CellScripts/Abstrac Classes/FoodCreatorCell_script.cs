using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodCreatorCell_script : Cell_script {

    public void Awake()
    {
        HalfSecondLooper.Add(CreateFoodLoop);
    }

    private void CreateFoodLoop() {
        Energy_script.Feed(CreateFood());
    }

    public abstract int CreateFood();
}
