using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class Cell_script : MonoBehaviour {

    public const int nutVal = 5;
    public const int FoodCost = 1;
    public DNAO DNAO { get; set; }

    protected const int MaxFood = FoodCost * 3;
    protected int CurrentFood = MaxFood;

    private Looper HalfSecondLooper;
    private Looper OneSecondLooper;
    private Looper FiveSecondLooper;

    public Attach_script Attach_Script;

    private CoreCell_script CoreCell_script;

    public virtual void Awake()
    {
        HalfSecondLooper = new Looper(HalfSecondLoop, 0.5f);
        OneSecondLooper = new Looper(OneSecondLoop, 1f);
        FiveSecondLooper = new Looper(FiveSecondLoop, 5f);
    }

    public void Start()
    {
        Attach_Script.Attach();

        GameObject core = Attach_Script.RecursiveTagSearch("CoreCell");

        if (core != null)
            CoreCell_script = core.GetComponent<CoreCell_script>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        Food_script fs = col.GetComponent<Food_script>();

        if (fs != null)
            Feed(fs.Eat());
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
        int exessFood = CurrentFood - MaxFood;
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
