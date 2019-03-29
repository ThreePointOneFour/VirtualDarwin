using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class Energy_script : MonoBehaviour
{
    public Cell_script Cell_script;
    public AnchorHub_script AnchorHub_script;

    public const int FoodCost = 1;
    public const int MaxFood = FoodCost * 3;
    public const int StartFood = MaxFood;

    private int CurrentFood = StartFood;

    public void Awake()
    {
        Cell_script.FiveSecondLoop += HungerLoop;
        Cell_script.HalfSecondLoop += PassFoodOnLoop;
    }

    public void HungerLoop() {
        Hunger(FoodCost);
    }

    public void PassFoodOnLoop() {
        int exessFood = GetExess();
        if (exessFood > 0)
        {
            PassFoodOn(exessFood);
            Hunger(exessFood);
        }
    }

    public void Hunger(int amount)
    {
        CurrentFood -= amount;
        if (CurrentFood < 0)
            Destroy(gameObject);
    }

    public void Feed(int amount)
    {
        CurrentFood += amount;
    }

    public int GetCurrentFood()
    {
        return CurrentFood;
    }

    public int GetStartFood() {
        return StartFood;
    }

    protected virtual int GetExess()
    {
        return CurrentFood - MaxFood;
    }

    private void PassFoodOn(int amount)
    {
        IDictionary<PhysicsU.Directions, GameObject> Coupleds = AnchorHub_script.GetCoupleds();
        int attachedCnt = AnchorHub_script.GetCoupledCount();
        foreach (KeyValuePair<PhysicsU.Directions, GameObject> e in Coupleds)
        {
            GameObject cell = e.Value;

            if (cell == null) continue;

            int give = Mathf.FloorToInt(amount / attachedCnt);
            cell.GetComponent<Energy_script>().Feed(give);
        }

    }
}
