using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControll_script : MonoBehaviour {

    private readonly float[] modes = {0.1f, 0.5f, 1f, 2f, 5f, 10f };
    private int currentMode = 2;

    private void Start()
    {
    }

    private void SetSpeed(int mode)
    {
        currentMode = mode;
        Time.timeScale = modes[mode];
    }

}
