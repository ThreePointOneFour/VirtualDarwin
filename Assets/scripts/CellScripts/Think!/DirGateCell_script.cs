using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

public class DirGateCell_script : ConductorCell_script
{
    public PhysicsU.Directions gateDirection;

    public override void ConductorUpdate()
    {
        IDictionary<PhysicsU.Directions, ConductorCell_script> Conductors = GetAttachedConductors();

        if (!Conductors.ContainsKey(gateDirection)) return;

        if (Conductors[gateDirection].GetActive())
            SetActive(true);

    }
}
