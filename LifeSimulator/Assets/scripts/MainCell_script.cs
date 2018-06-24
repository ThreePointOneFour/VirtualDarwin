using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCell_script : Cell_script {

	protected override void CustomStart() {
		DNA_script.BuildOrganism ();
	}
}
