using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class Cell_script : MonoBehaviour {

    private DNA DNA = null;
    public const int nutVal = 5;

    private Looper HalfSecondLooper;
    private Looper OneSecondLooper;
    private Looper FiveSecondLooper;

    public System.Action HalfSecondLoop;
    public System.Action OneSecondLoop;
    public System.Action FiveSecondLoop;

    public AnchorHub_script AnchorHub_script;
    public Energy_script Energy_script;

    public virtual void Awake()
    {

        HalfSecondLooper = new Looper(HalfSecondLoop, 0.5f);
        OneSecondLooper = new Looper(OneSecondLoop, 1f);
        FiveSecondLooper = new Looper(FiveSecondLoop, 5f);
    }

    public void Update()
    {
        float dtime = Time.deltaTime;

        HalfSecondLooper.Loop(dtime);
        OneSecondLooper.Loop(dtime);
        FiveSecondLooper.Loop(dtime);
    }

    public virtual void OnDestroy()
    {
        print(gameObject + "died");
        Object Food = Resources.Load("Food_prefab");
        GameObject remains = Instantiate(Food, transform.position, Quaternion.identity) as GameObject;

        int FoodValue = nutVal + Energy_script.GetCurrentFood();
        remains.GetComponent<Food_script>().nutritionValue = FoodValue;
    }

    public DNA GetDNA() {
        return DNA;
    }

    public void SetDNA(DNA DNA) {
        if(DNA == null)
            this.DNA = DNA;
    }

    public GameObject GetOrganism() {
        return transform.parent.gameObject;
    }

    public int GetStartCost()
    {
        return nutVal + Energy_script.GetStartFood();
    }
}
