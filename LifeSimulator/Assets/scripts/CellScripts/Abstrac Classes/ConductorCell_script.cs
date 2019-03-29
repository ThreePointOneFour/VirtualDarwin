using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public abstract class ConductorCell_script : Cell_script {

    private bool Active;
    private bool ActiveToBeSet;

    public abstract void ConductorUpdate();

    public void Awake()
    {
        HalfSecondLooper.Add(ConductorUpdate);
    }

    protected void LateUpdate()
    {
        if(Active != ActiveToBeSet) {
            Active = ActiveToBeSet;
        }
    }

    protected IDictionary<PhysicsU.Directions, ConductorCell_script> GetAttachedConductors()
    {
        IDictionary<PhysicsU.Directions, ConductorCell_script> ret = new Dictionary<PhysicsU.Directions, ConductorCell_script>();
        IDictionary<PhysicsU.Directions, GameObject> Coupleds = AnchorHub_script.GetCoupleds();
        foreach (KeyValuePair<PhysicsU.Directions, GameObject> e in Coupleds)
        {
            GameObject go = e.Value;

            ConductorCell_script conductor = go.GetComponent<ConductorCell_script>();
            if (conductor != null)
                ret.Add(e.Key, conductor);
        }
        return ret;
    }

    protected int GetAttachedActiveCnt()
    {
        int attachedActive = 0;
        IDictionary<PhysicsU.Directions, ConductorCell_script> AttachedConductors = GetAttachedConductors();
        foreach (KeyValuePair<PhysicsU.Directions, ConductorCell_script> e in AttachedConductors)
        {
            if (e.Value.GetActive())
                attachedActive++;;
        }
        return attachedActive;
    }

    protected void SetActive(bool active) {
        ActiveToBeSet = active;
    }

    public bool GetActive() {
        return Active;
    }
}
