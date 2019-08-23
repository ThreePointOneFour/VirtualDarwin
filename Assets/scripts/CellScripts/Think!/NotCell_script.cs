using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotCell_script : ConductorCell_script
{

    public override void ConductorUpdate()
    {
        if (GetAttachedActiveCnt() > 0)
            SetActive(false);
        else
            SetActive(true);
    }
}
