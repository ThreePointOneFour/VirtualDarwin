using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndCell_script : ConductorCell_script {

    public override void ConductorUpdate()
    {
        if (GetAttachedActiveCnt() >= 1)
            SetActive(true);
        else
            SetActive(false);
    }
}
