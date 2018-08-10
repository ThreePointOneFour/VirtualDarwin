using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class CoreCell_script : Cell_script {

    public int startFood = 10;
    private int currentFood;
    private readonly int savetyFood = 5;

    private PrefabLoaderWrapper_script pl;
    public float MutateProbability = 0.2f;

    private int birthCost = int.MaxValue;
    private Looper foodClock;

    private void Awake()
    {
        pl = PrefabLoaderWrapper_script.GetPL();
    }

    public void Start()
    {
        birthCost = DNAO.CalcBirthReq(pl);
        foodClock = new Looper(FoodClock, 1.0f);
    }

    public void Update()
    {
        foodClock.Loop(Time.deltaTime);

        CheckBirth();
        CheckDead();
    }

    public void Feed(int amount)
    {
        currentFood += amount;
    }
    public void Hunger(int amount)
    {
        currentFood -= amount;
    }
    private void FoodClock()
    {
        Hunger(1);
    }

    private void CheckBirth()
    {
        if (currentFood >= birthCost + savetyFood)
        {
            Birth();
            Hunger(birthCost);
        }
    }

    private void CheckDead()
    {
        if (currentFood <= 0)
            Destroy(gameObject);
    }

    public void Birth()
    {
        string newDNAO = DNAO.Mutate(MutateProbability);

        GameObject seedprefab = pl.Load("Seed_prefab");
        Vector2 source = new Vector2(transform.position.x, transform.position.y);
        Vector2 pos = source + Random.insideUnitCircle.normalized * 5;
        GameObject seed = Instantiate(seedprefab, pos, Quaternion.identity) as GameObject;
        seed.GetComponent<CoreCell_script>().DNAO = new DNAO(newDNAO);
    }
}
