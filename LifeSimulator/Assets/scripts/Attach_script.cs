using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach_script : MonoBehaviour {

    private GameObject[] Attachments;

    public bool searching { get; private set; }

    private void Awake()
    {
        searching = false;
    }

    public void Attach(string layerMask, bool siblingsOnly = false)
    {
        Attachments = new GameObject[4];

        float length = 0.1F + GetComponent<BoxCollider2D>().size.x / 2;
        LayerMask mask = LayerMask.GetMask(layerMask);
        //right,up,left,down
        Vector2[] dirs = new Vector2[] { transform.right, transform.up, -transform.right, -transform.up };

        for (int i = 0; i < dirs.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirs[i], length, mask.value);
            if (!hit) continue;
            if(siblingsOnly)
                if (hit.transform.parent.gameObject != transform.parent.gameObject)
                    continue;

            GameObject go = hit.collider.gameObject;
            Attachments[i] = go;
            FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
            fj.connectedBody = go.GetComponent<Rigidbody2D>(); ;
        }
    }

    public GameObject RecursiveTagSearch(string searchTag)
    {
        if (Attachments == null)
        {
            throw new System.Exception("Attach before searching");
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

    public GameObject[] GetAttachments()
    {
        return Attachments;
    }
}
