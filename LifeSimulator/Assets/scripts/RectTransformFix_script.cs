using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformFix_script : MonoBehaviour {

    public RectTransform rt;
    public Vector3 RectPos;

	// Use this for initialization
	void Start () {
        rt.anchoredPosition = RectPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
