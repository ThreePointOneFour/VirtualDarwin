using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorCell_script : ConductorCell_script {

    public override void ConductorUpdate()
    {
        if (GetAttachedActiveCnt()%2 != 0)
            SetActive(true);
        else
            SetActive(false);
    }
}
