using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrCell_script : ConductorCell_script
{

    public override void ConductorUpdate()
    {
        if (GetAttachedActiveCnt() > 0)
            SetActive(true);
        else
            SetActive(false);
    }
}
