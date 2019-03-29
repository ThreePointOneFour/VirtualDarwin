using KitchenSink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_script : MonoBehaviour {

    private LayerMask layerMask;

    public GameObject cell;
    private GameObject coupled = null;
    private FixedJoint2D fixedJoint2D;
    public PhysicsU.Directions dir;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Anchor"); 
        AttempCoupling();

        CreateJoint();
    }

    public void AttempCoupling() {
        Vector2 direction = PhysicsU.Dir2Vec(dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, PhysicsU.Dir2Vec(dir), 0.2f, layerMask.value);

        if (!hit) return;
        if (hit.collider.gameObject == gameObject) return;

        GameObject Anchor = hit.collider.gameObject;
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

        if (fixedJoint2D == null) CreateJoint();
        fixedJoint2D.connectedBody = anchor_script.cell.GetComponent<Rigidbody2D>();
        
        anchor_script.Couple(gameObject);
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

    public GameObject GetCoupledCell() {
        GameObject coupled = GetCoupled();
        if (coupled == null) return null;
        else return coupled.GetComponent<Anchor_script>().cell;
    }

    private void CreateJoint() {
        fixedJoint2D = cell.AddComponent<FixedJoint2D>();
        fixedJoint2D.anchor = transform.localPosition;
    }
}
