using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCell_script : ConductorCell_script {

    public enum Modes {r,g,b};
    public Modes mode = Modes.r;

    private readonly int triggerTresh = 100;
    public float length = 10.0f;

    public override void ConductorUpdate()
    {
        if (See())
            SetActive(true);
        else
            SetActive(false);
    }

    private bool See()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, length);
        if (!hit) return false;

        GameObject go = hit.collider.gameObject;
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr == null) return false;

        float comp = 0;

        switch (mode)
        {
            case Modes.r:
                comp = sr.color.r;
                break;
            case Modes.g:
                comp = sr.color.g;
                break;
            case Modes.b:
                comp = sr.color.b;
                break;
        }
        return (Mathf.FloorToInt(comp) > triggerTresh);
    }
}
