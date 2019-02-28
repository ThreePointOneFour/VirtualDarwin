using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class Cell_script : MonoBehaviour {

    public const int nutVal = 5;
    public const int FoodCost = 1;
    public DNAO DNAO { get; set; }

    private const int MaxFood = FoodCost * 3;
    private int CurrentFood = MaxFood;

    private Looper HalfSecondLooper;
    private Looper OneSecondLooper;
    private Looper FiveSecondLooper;

    private Attach_script Attach_Script;

    public virtual void Awake()
    {
        HalfSecondLooper = new Looper(HalfSecondLoop, 0.5f);
        OneSecondLooper = new Looper(OneSecondLoop, 1f);
        FiveSecondLooper = new Looper(FiveSecondLoop, 5f);
    }

    public virtual void Start()
    {
        Attach_Script = GetComponent<Attach_script>();
        Attach_Script.Attach();

        GameObject core = Attach_Script.RecursiveTagSearch("CoreCell");
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
        remains.GetComponent<Food_script>().nutritionValue = nutVal + CurrentFood;
    }

    public void Hunger(int amount) {
        CurrentFood -= amount;
        if (CurrentFood < 0)
            Destroy(gameObject);
    }

    public void Feed(int amount) {
        CurrentFood += amount;
    }

    public int GetCurrentFood() {
        return CurrentFood;
    }

    protected virtual int GetExess() {
        return CurrentFood - MaxFood;
    }

    private void PassFoodOn(int amount) {
        GameObject[] AttachedCells = Attach_Script.GetAttachments();
        int attachedCnt = Attach_Script.GetAttachedCnt();
        foreach (GameObject cell in AttachedCells) {
            if (cell == null) continue;
            int give = Mathf.FloorToInt(amount / attachedCnt);
            cell.GetComponent<Cell_script>().Feed(give);
        }

    }

    private bool IsConnected()
    {
        GameObject core = Attach_Script.RecursiveTagSearch("CoreCell");
        if (core == null)
            return false;
        else
            return true;

    }

    public int GetStartCost() {
        return nutVal + MaxFood;
    }

    protected virtual void HalfSecondLoop() {
        int exessFood = GetExess();
        if (exessFood > 0) {
            PassFoodOn(exessFood);
            Hunger(exessFood);
        }
    }
    protected virtual void OneSecondLoop() { }
    protected virtual void FiveSecondLoop() {
        Hunger(FoodCost);
    }
}
