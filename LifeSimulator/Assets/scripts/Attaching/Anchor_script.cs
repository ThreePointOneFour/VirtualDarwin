using KitchenSink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_script : MonoBehaviour {

    private LayerMask layerMask = LayerMask.GetMask("Anchor");


    public readonly GameObject cell;
    private GameObject coupled = null;
    private FixedJoint2D fixedJoint2D;
    public readonly PhysicsU.Directions dir;

    private void Start()
    {
        fixedJoint2D = cell.AddComponent<FixedJoint2D>();
        fixedJoint2D.anchor = transform.position;
    }

    public void AttempCoupling() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, PhysicsU.Dir2Vec(dir), layerMask.value);

        if (hit.transform.gameObject == gameObject) return;

        GameObject Anchor = hit.transform.gameObject;
        Anchor_script anchor_script = GetAnchorScript(Anchor);

        //check if Raycast hit foreign organism
        if (anchor_script.cell.GetComponent<Cell_script>().GetOrganism() == cell.GetComponent<Cell_script>().GetOrganism())
            return;

        Couple(Anchor);
    }

    public void Couple(GameObject Anchor) {
        if (coupled != null) return;
        Anchor_script anchor_script = GetAnchorScript(Anchor);

        coupled = Anchor;
        fixedJoint2D.connectedBody = anchor_script.cell.GetComponent<Rigidbody2D>();

        anchor_script.Couple(gameObject);
    }

    public void Uncouple() {
        if (coupled == null)
            return;
        Anchor_script anchor_script = GetAnchorScript(coupled);

        fixedJoint2D.connectedBody = null;
        coupled = null;
        anchor_script.Uncouple();
    }

    private Anchor_script GetAnchorScript(GameObject Anchor) {
        Anchor_script anchor_script = Anchor.GetComponent<Anchor_script>();
        if (Anchor == null)
        {
            throw new System.ArgumentException("The given gameObject didn't contain a anchor script");
        }
        return anchor_script;
    }

    private GameObject GetCoupled() {
        return coupled;
    }

}
