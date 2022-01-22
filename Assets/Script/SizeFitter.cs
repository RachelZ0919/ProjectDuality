using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SizeFitter : MonoBehaviour
{
    public RectTransform rect;
    public float offset;

    private RectTransform myRect;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = myRect.sizeDelta;
        size.x = rect.sizeDelta.x + offset;
        myRect.sizeDelta = size;
    }
}
