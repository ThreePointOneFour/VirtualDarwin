using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class Attach_script : MonoBehaviour
{

    private GameObject[] Attachments;

    private string layerMask = "Anchor";
    public bool searching { get; private set; }

    private void Awake()
    {
        searching = false;
    }

    public void Attach()
    {
        Attachments = new GameObject[4];

        float length = 0.1F + GetComponent<BoxCollider2D>().size.x / 2;
        LayerMask mask = LayerMask.GetMask(layerMask);
        //right,up,left,down
        Vector2[] dirs = new Vector2[] { transform.right, transform.up, -transform.right, -transform.up };

        for (int i = 0; i < dirs.Length; i++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dirs[i], length, mask.value);

            if (hits.Length != 2) continue;

            RaycastHit2D ownHit = hits[0];
            RaycastHit2D foreignHit = hits[1];

            GameObject cell = foreignHit.transform.gameObject;
            GameObject anchor = foreignHit.collider.gameObject;

            //check if Raycast hit foreign cell
            if (cell.transform.parent.gameObject != transform.parent.gameObject)
                continue;

            //check if Raycast hit own cell
            if (ownHit.transform.gameObject != gameObject) {
                continue;
            }

            Attachments[i] = cell;
            FixedJoint2D fj = cell.AddComponent<FixedJoint2D>();
            fj.connectedBody = GetComponent<Rigidbody2D>(); ;
            Debug.Log(gameObject.name + " attached to " + cell.name);
        }
    }

    public GameObject RecursiveTagSearch(string searchTag)
    {
        if (Attachments == null)
        {
            Attach();
        }


        if (tag == searchTag)
            return this.gameObject;
        else if (searching == true)
            return null;
        else
        {
            searching = true;

            GameObject[] Attachments = GetAttachments();

            foreach (GameObject obj in Attachments)
            {
                if (obj == null) continue;

                GameObject found;
                Attach_script At = obj.GetComponent<Attach_script>();
                if (At == null) continue;
                if ((found = At.RecursiveTagSearch(searchTag)) != null)
                {
                    searching = false;
                    return found;
                }
            }

            searching = false;
            return null;
        }
    }

    public int GetAttachedCnt() {
        int cnt = 0;
        foreach (GameObject cell in Attachments) {
            if (cell != null) cnt++;
        }
        return cnt;
    }

    public GameObject[] GetAttachments()
    {
        return Attachments;
    }
}
