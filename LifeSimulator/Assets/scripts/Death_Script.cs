using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Script : MonoBehaviour {

    private Cell_script Cell_script;

    private bool checking = false;

    private void Start()
    {
        Cell_script = GetComponent<Cell_script>();
    }

    void Update()
    {
        if (!IsConnected())
            Destroy(gameObject);
    }

    void OnDestroy()
    {
        
    }

    public bool IsConnected()
    {
        if (tag == "CoreCell")
            return true;
        else if (checking == true)
            return false;
        else
        {
            checking = true;

            GameObject[] Attachments = Cell_script.GetAttachments();

            foreach (GameObject obj in Attachments)
            {
                if (obj == null) continue;

                Death_Script ds = obj.GetComponent<Death_Script>();
                if (ds == null) continue;

                if (ds.IsConnected() == true)
                {
                    checking = false;
                    return true;
                }
            }

            checking = false;
            return false;
        }
    }
}
