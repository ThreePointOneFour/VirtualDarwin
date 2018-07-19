using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_script : MonoBehaviour {

    private GameObject[] Attachments = new GameObject[4];

	private void Start() {

        Attach();
	}

    void OnDestroy()
    {
        print(gameObject + " died!");
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

    public GameObject[] GetAttachments()
    {
        return Attachments;
    }
}
