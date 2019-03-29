using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class AnchorHub_script : MonoBehaviour {

    [System.Serializable]
    public class DirAnchorEntry
    {
        public PhysicsU.Directions Direction;
        public GameObject Anchor;
    }

    public DirAnchorEntry[] AnchorsInput;
    private IDictionary<PhysicsU.Directions, GameObject> Anchors = new Dictionary<PhysicsU.Directions, GameObject>();

    public void Start()
    {
        FillAnchors();
    }


    public int GetCoupledCount()
    {
        IDictionary<PhysicsU.Directions, GameObject> Coupleds = GetCoupleds();
        int cnt = 0;
        foreach (KeyValuePair<PhysicsU.Directions, GameObject> e in Anchors)
        {
            if (e.Value != null)
                cnt++;
        }
        return cnt;
    }

    public GameObject GetCoupledAt(PhysicsU.Directions anchorDir) {

        IDictionary<PhysicsU.Directions, GameObject> Coupleds = GetCoupleds();
        return Coupleds[anchorDir];
    }

    public IDictionary<PhysicsU.Directions, GameObject> GetCoupleds()
    {
        FillAnchors();

        IDictionary<PhysicsU.Directions, GameObject> ret = new Dictionary<PhysicsU.Directions, GameObject>();
        foreach (KeyValuePair<PhysicsU.Directions, GameObject> e in Anchors)
        {
            ret.Add(e.Key, GetAnchorCouple(e.Value));
        }
        return ret;
    }

    private void FillAnchors() {
        if (Anchors.Count > 0) return;

        foreach (DirAnchorEntry DAE in AnchorsInput)
        {
            Anchors.Add(DAE.Direction, DAE.Anchor);
            DAE.Anchor.GetComponent<Anchor_script>().AttempCoupling();
        }
    }

    private GameObject GetAnchorCouple(GameObject Anchor) {
        Anchor_script Anchor_script = Anchor.GetComponent<Anchor_script>();
        return Anchor_script.GetCoupled();

    }
}
