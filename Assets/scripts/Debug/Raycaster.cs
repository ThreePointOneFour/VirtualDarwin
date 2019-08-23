using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public string layerMaskString;
    // Start is called before the first frame update
    void Start()
    {
        LayerMask layerMask = LayerMask.GetMask(layerMaskString);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 15f, layerMask.value);


    }
}
