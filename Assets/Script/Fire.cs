using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float quenchTime;//熄灭所用时间
    private float value;


    private void Start()
    {
        value = quenchTime;
    }

    /// <summary>
    /// 灭火
    /// </summary>
    public void Quench()
    {
        value -= Time.deltaTime;
        if (value < 0)//火完全被灭时
        {
            Debug.Log("successed");
            Destroy(gameObject);
        }
    }
}
