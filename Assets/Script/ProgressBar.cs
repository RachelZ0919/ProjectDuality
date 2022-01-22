using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Range(0,100)] public float value = 0;
    public RectTransform continousBar;
    public Vector2 minMaxWidth;
    public Transform[] discreteBar;
    public ProgressBarKind barKind;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        value = Mathf.Clamp(value, 0, 100);

        if (barKind == ProgressBarKind.continuous)
        {
            continousBar.gameObject.SetActive(true);
            foreach(var bar in discreteBar)
            {
                bar.gameObject.SetActive(false);
            }

            Vector2 size = continousBar.sizeDelta;
            size.x = minMaxWidth.x + (minMaxWidth.y - minMaxWidth.x) * value / 100;
            continousBar.sizeDelta = size;
        }
        else
        {
            continousBar.gameObject.SetActive(false);
            for (var i = 0; i < 3; i++)
            {
                if (i < (int)value / 33)
                {
                    if (!discreteBar[i].gameObject.activeSelf)
                    {
                        discreteBar[i].gameObject.SetActive(true);
                        if (animator) animator.SetTrigger("Add");
                    }
                }
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
