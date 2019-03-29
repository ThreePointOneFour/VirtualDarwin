using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class Energy_script : MonoBehaviour
{
    public Cell_script Cell_script;
    public AnchorHub_script AnchorHub_script;

    public int FoodCost = 1;
    public int MaxFood = -1;
    public int StartFood = -1;

    private int CurrentFood;

    public void Start()
    {
        if (MaxFood == -1) MaxFood = FoodCost * 3;
        if (StartFood == -1) StartFood = FoodCost * 3;

        CurrentFood = StartFood;

        Cell_script.FiveSecondLooper.Add(HungerLoop);
        Cell_script.HalfSecondLooper.Add(PassFoodOn);
    }

    public void HungerLoop() {
        Hunger(FoodCost);
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

    private void PassFoodOn()
    {
        IDictionary<PhysicsU.Directions, GameObject> Coupleds = AnchorHub_script.GetCoupleds();
        int attachedCnt = AnchorHub_script.GetCoupledCount();

        int exess = GetExess();

        if (exess < 1) return;
        if (attachedCnt == 0) return;

        int give = Mathf.CeilToInt(exess / attachedCnt);
        foreach (KeyValuePair<PhysicsU.Directions, GameObject> e in Coupleds)
        {
            exess = GetExess();
            if (exess < 1) return;

            GameObject cell = e.Value;

            if (cell == null) continue;

            FeedCell(cell, give);
        }

    }

    private void FeedCell(GameObject cell, int amount) {
        cell.GetComponent<Energy_script>().Feed(amount);
        Hunger(amount);
    }
}
