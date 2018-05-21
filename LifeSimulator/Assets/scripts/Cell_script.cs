using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_script : MonoBehaviour {

	protected DNA_script DNA_script;

	private void Start() {
		DNA_script = this.GetComponent<DNA_script> ();
		CustomStart ();
	}

	protected virtual void CustomStart() {}

}
