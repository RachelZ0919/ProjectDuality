using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float time = 1;

    private float progress = 0;
    private bool play = false;

    private void Update()
    {
        if (play)
        {
            progress += Time.deltaTime / time;
            progress = Mathf.Clamp01(progress);
            if(progress >= 1)
            {
                play = false;
                progress = 0;
            }
            transform.localPosition = Vector3.Lerp(startPos, endPos, progress);
        }

    }

    public void StartShine()
    {
        play = true;
    }
}
