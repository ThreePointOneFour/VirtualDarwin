using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class BirthCell_script : Cell_script {

    private PrefabLoaderWrapper_script pl;
    public float MutateProbability = 0.2f;

    private int birthCost = int.MaxValue;

    public override void Awake()
    {
        base.Awake();
        pl = PrefabLoaderWrapper_script.GetPL();

        FiveSecondLoop += CheckBirth;
    }

    private void CheckBirth()
    {
        int birthCost = GetBirthCost();
        if (Energy_script.GetCurrentFood() >= birthCost + Energy_script.FoodCost * 2)
        {
            Birth();
            Energy_script.Hunger(birthCost);
        }
    }

    private int GetBirthCost()
    {
        if (birthCost == int.MaxValue)
            birthCost = GetDNA().GetBirthCost();
        return birthCost;
    }

    public void Birth()
    {
        DNA newDNA = GetDNA().Duplicate();

        GameObject seedprefab = pl.Load("Seed");
        Vector2 source = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos = source + Random.insideUnitCircle.normalized * 5;
        GameObject seed = Instantiate(seedprefab, pos, Quaternion.identity) as GameObject;
        seed.GetComponent<Seed_script>().SetDNA(newDNA);
    }
}
