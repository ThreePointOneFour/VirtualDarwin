using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConductor_Cell : ConductorCell_script
{

    public override void OnConductorLoop()
    {
        if (GetAttachedActiveCnt() > 0)
            Active = true;
        else
            Active = false;
    }
}
