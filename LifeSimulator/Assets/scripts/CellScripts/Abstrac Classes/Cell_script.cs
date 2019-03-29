using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class Cell_script : MonoBehaviour {

    private DNA DNA = null;
    public const int nutVal = 5;

    public Looper HalfSecondLooper = new Looper(0.5f);
    public Looper OneSecondLooper = new Looper(1f);
    public Looper FiveSecondLooper = new Looper(5f);

    public AnchorHub_script AnchorHub_script;
    public Energy_script Energy_script;

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
        if(this.DNA == null)
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
