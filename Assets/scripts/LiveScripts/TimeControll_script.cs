using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControll_script : MonoBehaviour {

    public Text writeTo;

    public void SetSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    public void WriteToText(float speed)
    {
        writeTo.text = "" + speed;
    }

}
