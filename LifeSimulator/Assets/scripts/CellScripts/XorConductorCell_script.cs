using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorConductorCell_script : ConductorCell_script {

    public override void OnConductorLoop()
    {
        if (GetAttachedActiveCnt() > 0)
            Active = false;
        else
            Active = true;
    }
}
