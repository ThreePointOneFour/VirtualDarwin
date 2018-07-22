using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer_script : MonoBehaviour {

    public int seed;

	void Awake () {
        if (seed == 0)
            seed = Random.Range(int.MinValue, int.MaxValue);

        Random.InitState(seed);
	}
}
