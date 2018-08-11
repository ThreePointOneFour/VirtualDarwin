using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndConductorCell_script : ConductorCell_script {

    public override void OnConductorLoop()
    {
        if (GetAttachedActiveCnt() >= 2)
            Active = true;
        else
            Active = false;
    }
}
