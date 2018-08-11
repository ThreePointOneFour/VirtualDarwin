using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConductorCell_script : Cell_script {

    public bool Active { get; protected set; }

    public abstract void OnConductorLoop();

    protected override void HalfSecondLoop()
    {
        OnConductorLoop();
    }

    protected List<ConductorCell_script> GetAttachedConductor()
    {
        List<ConductorCell_script> ret = new List<ConductorCell_script>();
        GameObject[] Attachments = Attach_Script.GetAttachments();
        foreach (GameObject go in Attachments)
        {
            if (go == null) continue;
            ConductorCell_script conductor = (go.GetComponent<ConductorCell_script>();
            if (conductor != null )
                ret.Add(conductor);
        }
        return ret;
    }
    protected int GetAttachedActiveCnt()
    {
        int attachedActive = 0;
        List<ConductorCell_script> acs = GetAttachedConductor();
        foreach (ConductorCell_script conductor in acs)
        {
            if (conductor.Active)
                attachedActive++;;
        }
        return attachedActive;
    }
}
