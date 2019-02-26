using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndAndCell_script : ConductorCell_script
{

    public override void ConductorUpdate()
    {
        if (GetAttachedActiveCnt() >= 2)
            SetActive(true);
        else
            SetActive(false);
    }
}
