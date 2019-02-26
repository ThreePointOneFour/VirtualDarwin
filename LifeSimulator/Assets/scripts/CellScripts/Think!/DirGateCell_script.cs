using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirGateCell_script : ConductorCell_script
{
    public enum Dir {right, up, left, down };
    public Dir gateDirection;

    public override void ConductorUpdate()
    {
        ConductorCell_script dirCon = GetAttachedConductor()[(int)gateDirection];
        if (dirCon != null && dirCon.GetActive())
            SetActive(true);
    }
}
