using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class Cell_script : MonoBehaviour {

    public int nutVal = 5;
    public DNAO DNAO { get; set; }

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
        Attach_Script.Attach("Cell", true);

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

        if (!IsConnected())
        {
            Destroy(gameObject);
        }
            
    }

    public virtual void OnDestroy()
    {
        print(gameObject + "died");
        Object Food = Resources.Load("Food_prefab");
        GameObject remains = Instantiate(Food, transform.position, Quaternion.identity) as GameObject;
        remains.GetComponent<Food_script>().nutritionValue = nutVal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        Food_script es = col.GetComponent<Food_script>();

        if ((es != null) && CoreCell_script != null)
            CoreCell_script.Feed(es.Eat());
    }

    private bool IsConnected()
    {
        GameObject core = Attach_Script.RecursiveTagSearch("CoreCell");
        if (core == null)
            return false;
        else
            return true;

    }

    protected virtual void HalfSecondLoop() { }
    protected virtual void OneSecondLoop() { }
    protected virtual void FiveSecondLoop() { }
}
