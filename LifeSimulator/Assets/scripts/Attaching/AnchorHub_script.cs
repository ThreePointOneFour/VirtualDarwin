using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorHub_script : MonoBehaviour {

    private string layerMask = "Anchor";
    public bool searching { get; private set; }

    private void Awake()
    {
        searching = false;
    }

    public int GetAttachedCnt()
    {
        GameObject[] Attachments = GetAttachments();
        int cnt = 0;
        foreach (GameObject cell in Attachments)
        {
            if (cell != null) cnt++;
        }
        return cnt;
    }

    public GameObject[] GetAttachments()
    {
        return null;
    }
}
