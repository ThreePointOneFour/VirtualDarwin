using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConductorCell_script : Cell_script {

    private bool Active;
    private bool ActiveToBeSet;

    public abstract void ConductorUpdate();

    protected override void HalfSecondLoop()
    {
        ConductorUpdate();
    }

    protected void LateUpdate()
    {
        if(Active != ActiveToBeSet) {
            Active = ActiveToBeSet;
        }
    }

    protected List<ConductorCell_script> GetAttachedConductor()
    {
        List<ConductorCell_script> ret = new List<ConductorCell_script>();
        GameObject[] Attachments = Attach_Script.GetAttachments();
        foreach (GameObject go in Attachments)
        {
            if (go == null)
            {
                ret.Add(null);
            }
            else {
                ConductorCell_script conductor = go.GetComponent<ConductorCell_script>();
                ret.Add(conductor);
            }
        }
        return ret;
    }

    protected int GetAttachedActiveCnt()
    {
        int attachedActive = 0;
        List<ConductorCell_script> acs = GetAttachedConductor();
        foreach (ConductorCell_script conductor in acs)
        {
            if (conductor == null)
                continue;
            if (conductor.GetActive())
                attachedActive++;;
        }
        return attachedActive;
    }

    protected void SetActive(bool active) {
        ActiveToBeSet = active;
    }

    public bool GetActive() {
        return Active;
    }
}
