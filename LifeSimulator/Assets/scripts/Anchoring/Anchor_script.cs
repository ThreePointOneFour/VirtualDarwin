using KitchenSink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_script : MonoBehaviour {

    private LayerMask layerMask;


    public GameObject cell;
    private GameObject coupled = null;
    private FixedJoint2D fixedJoint2D;
    public readonly PhysicsU.Directions dir;

    private void Start()
    {
        Debug.Log(transform.localPosition);
        AttempCoupling();
        layerMask = LayerMask.GetMask("Anchor");

        fixedJoint2D = cell.AddComponent<FixedJoint2D>();
        fixedJoint2D.anchor = transform.localPosition;
    }

    public void AttempCoupling() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, PhysicsU.Dir2Vec(dir), layerMask.value);

        if (!hit) return;
        if (hit.transform.gameObject == gameObject) return;

        GameObject Anchor = hit.transform.gameObject;
        Anchor_script anchor_script = Anchor.GetComponent<Anchor_script>();

        //check if Raycast hit foreign organism
        if (anchor_script.cell.GetComponent<Cell_script>().GetOrganism() != cell.GetComponent<Cell_script>().GetOrganism())
            return;

        Couple(Anchor);
    }

    public void Couple(GameObject Anchor) {
        if (coupled != null) return;
        Anchor_script anchor_script = Anchor.GetComponent<Anchor_script>();

        coupled = Anchor;
        fixedJoint2D.connectedBody = anchor_script.cell.GetComponent<Rigidbody2D>();

        anchor_script.Couple(gameObject);

        Debug.Log(gameObject + " of " + cell + " coupled with" + Anchor + " of " + anchor_script.cell);
    }

    public void Uncouple() {
        if (coupled == null)
            return;
        Anchor_script anchor_script = coupled.GetComponent<Anchor_script>();

        fixedJoint2D.connectedBody = null;
        coupled = null;
        anchor_script.Uncouple();
    }

    public GameObject GetCoupled() {
        return coupled;
    }

}
