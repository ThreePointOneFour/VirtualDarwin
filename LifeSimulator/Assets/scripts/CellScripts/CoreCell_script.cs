using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class CoreCell_script : Cell_script {

    public bool sterile = false;

    public int startFood;
    private int currentFood;
    private readonly int savetyFood = 5;

    private PrefabLoaderWrapper_script pl;
    public float MutateProbability = 0.2f;

    private int birthCost = int.MaxValue;

    public override void Awake()
    {
        base.Awake();
        currentFood = startFood;
        pl = PrefabLoaderWrapper_script.GetPL();
    }

    protected override void HalfSecondLoop()
    {
        if(!sterile) CheckBirth();
        CheckDead();
    }

    protected override void OneSecondLoop()
    {
        Hunger(1);
    }

    public override void OnDestroy()
    {
        print(gameObject + "died");
        Object Food = Resources.Load("Food_prefab");
        GameObject remains = Instantiate(Food, transform.position, Quaternion.identity) as GameObject;
        remains.GetComponent<Food_script>().nutritionValue = nutVal + currentFood;
    }

    public void Feed(int amount)
    {
        currentFood += amount;
    }
    public void Hunger(int amount)
    {
        currentFood -= amount;
    }

    private void CheckBirth()
    {
        int birthCost = GetBirthCost();
        if (currentFood >= birthCost + savetyFood)
        {
            Birth();
            Hunger(birthCost);
        }
    }
    private int GetBirthCost()
    {
        if (birthCost == int.MaxValue)
            birthCost = DNAO.CalcBirthReq(pl);
        return birthCost;
    }

    private void CheckDead()
    {
        if (currentFood <= 0)
            Destroy(gameObject);
    }

    public void Birth()
    {
        string newDNAO = DNAO.Mutate(MutateProbability);

        GameObject seedprefab = pl.Load("Seed");
        Vector2 source = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos = source + Random.insideUnitCircle.normalized * 5;
        GameObject seed = Instantiate(seedprefab, pos, Quaternion.identity) as GameObject;
        seed.GetComponent<Seed_script>().DNAO = new DNAO(newDNAO);
    }
}
