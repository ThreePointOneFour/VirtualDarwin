﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_script : MonoBehaviour {

    public int nutVal = 5;

    private GameObject[] Attachments = new GameObject[4];
    private Food_script Food_script;
    private bool coreSearching = false;

    private void Start() {
        Attach();

        GameObject core = SearchCore();

        if (core != null)
            Food_script = core.GetComponent<Food_script>();

    }

    void OnDestroy()
    {
        print(gameObject + " died!");
        Object Food = Resources.Load("Food_prefab");
        GameObject remains = Instantiate(Food, transform.position, Quaternion.identity) as GameObject;
        remains.GetComponent<eatable_script>().nutritionValue = nutVal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered" + collision.gameObject);
        GameObject col = collision.gameObject;
        eatable_script es = col.GetComponent<eatable_script>();

        if ((es != null) && Food_script != null)
            Food_script.Feed(es.eat());
    }

    private void Attach()
    {
        float length = 0.1F + GetComponent<BoxCollider2D>().size.x / 2;
        LayerMask mask = LayerMask.GetMask("Cell");
        //right,up,left,down
        Vector2[] dirs = new Vector2[] { transform.right, transform.up, -transform.right, -transform.up };

        for(int i=0; i < dirs.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirs[i], length, mask.value);
            if (hit)
            {
                GameObject go = hit.collider.gameObject;
                Attachments[i] = go;
                FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
                fj.connectedBody = go.GetComponent<Rigidbody2D>(); ;
            }
        }

        //print(Attachments[0] + "|" + Attachments[1] + "|" + Attachments[2] + "|" + Attachments[3]);
    }

    public GameObject SearchCore()
    {
        if (tag == "CoreCell")
            return this.gameObject;
        else if (coreSearching == true)
            return null;
        else
        {
            coreSearching = true;

            GameObject[] Attachments = GetAttachments();

            foreach (GameObject obj in Attachments)
            {
                if (obj == null) continue;

                GameObject core;
                if ((core = SearchCore()) != null)
                {
                    coreSearching = false;
                    return core;
                }
            }

            coreSearching = false;
            return null;
        }
    }

    public GameObject[] GetAttachments()
    {
        return Attachments;
    }
}
