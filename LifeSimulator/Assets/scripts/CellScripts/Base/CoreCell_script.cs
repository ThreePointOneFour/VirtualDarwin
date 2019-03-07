using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class CoreCell_script : Cell_script {

    private readonly int savetyFood = 5;

    private PrefabLoaderWrapper_script pl;
    public float MutateProbability = 0.2f;

    private int birthCost = int.MaxValue;

    public override void Awake()
    {
        base.Awake();
        pl = PrefabLoaderWrapper_script.GetPL();
    }

    protected override void FiveSecondLoop()
    {
        base.FiveSecondLoop();
        CheckBirth();
    }

    protected override int GetExess() {
        return base.GetExess() / 2;
    }

    private void CheckBirth()
    {
        int birthCost = GetBirthCost();
        if (GetCurrentFood() >= birthCost + savetyFood)
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
