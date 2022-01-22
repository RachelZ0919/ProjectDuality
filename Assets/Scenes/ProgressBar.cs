using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float value = 0;
    public float maxScale;
    public Transform continousBar;
    public Transform[] discreteBar;
    public ProgressBarKind barKind;
    void Start()
    {
        if (barKind == ProgressBarKind.continuous)
            continousBar.gameObject.SetActive(true);
    }
    void Update()
    {
        if (barKind == ProgressBarKind.continuous)
            continousBar.localScale = new Vector3(continousBar.localScale.x, value / 100 * maxScale, 1);
        else
        {
            for (var i = 0; i < 3; i++)
            {
                if (i < (int)value / 33)
                    discreteBar[i].gameObject.SetActive(true);
                else
                    discreteBar[i].gameObject.SetActive(false);
            }
        }
    }
}
public enum ProgressBarKind
{
    discrete, continuous
}
