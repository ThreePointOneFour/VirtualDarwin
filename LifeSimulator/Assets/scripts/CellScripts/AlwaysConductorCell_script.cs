using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysConductorCell_script : ConductorCell_script
{
    public override void OnConductorLoop()
    {
        Active = true;
    }
}
