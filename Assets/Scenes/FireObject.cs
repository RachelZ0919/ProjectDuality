using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public float quenchTime;
    public ProgressBar myProgressBar;

    private float value;


    private void Start()
    {
        value = quenchTime;
    }
    public void Quench()
    {
        value -= Time.deltaTime;
        myProgressBar.value = (1 - value / quenchTime) * 100;
        if (value < 0)
            Debug.Log("successed");
    }
}
