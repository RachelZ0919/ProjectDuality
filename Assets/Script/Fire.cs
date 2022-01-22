using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float quenchTime;//熄灭所用时间
    public float shakeRange = .2f;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private float minScalePercent = 0.3f;


    private float value;
    private Vector3 originalPosition;
    private float scaleRange;
    private float minScale;

    private bool getHit = false;
    private bool lastGetHit = false;

    private void Start()
    {
        value = quenchTime;
        originalPosition = spriteTransform.localPosition;
        float originalScale = spriteTransform.localScale.x;
        minScale = originalScale * minScalePercent;
        scaleRange = originalScale - minScale;

        spriteTransform.GetComponent<Animator>().Play("Fire", -1, Random.Range(0, 1f));
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// 灭火
    /// </summary>
    public void Quench(bool skill)
    {
        float shake = Random.Range(-shakeRange, shakeRange);
        Vector3 pos = originalPosition;
        originalPosition.x += shake;
        spriteTransform.localPosition = pos;
        getHit = true;

        value -= Time.deltaTime * (skill ? 10 : 1);
        spriteTransform.localScale = new Vector3(value / quenchTime * scaleRange + minScale, value / quenchTime * scaleRange + minScale, 1);
        if (value < 0)//火完全被灭时
        {
            Debug.Log("successed");
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (!lastGetHit && getHit)
        {
            if (smoke) smoke.Play();
        }
        else if (lastGetHit && !getHit)
        {
            if (smoke) smoke.Stop();
        }

        if (!getHit)
        {
            Vector3 pos = spriteTransform.localPosition;
            pos = Vector3.Lerp(pos, originalPosition, .3f);
            spriteTransform.localPosition = pos;
        }

        lastGetHit = getHit;
        getHit = false;
    }
}
