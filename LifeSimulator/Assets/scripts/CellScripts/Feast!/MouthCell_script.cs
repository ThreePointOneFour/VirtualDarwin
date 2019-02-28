using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthCell_script : Cell_script {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        Food_script fs = col.GetComponent<Food_script>();

        if (fs != null)
            Feed(fs.Eat());
    }
}
