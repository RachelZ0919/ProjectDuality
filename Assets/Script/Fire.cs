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
    [SerializeField] private float dieSpeed = 10;
    [SerializeField] private float dieDrag = .3f;

    public FireObject fireObject { get; set; }

    private float value;
    private Vector3 originalPosition;
    private float scaleRange;
    private float minScale;

    private bool getHit = false;
    private bool lastGetHit = false;

    private Vector2 vel;
    private bool dead = false;

    private Material mat;

    private void Awake()
    {
        mat = spriteTransform.GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        value = quenchTime;
        originalPosition = spriteTransform.localPosition;
        float originalScale = transform.localScale.x;
        minScale = originalScale * minScalePercent;
        scaleRange = originalScale - minScale;

        spriteTransform.GetComponent<Animator>().Play("Fire", -1, Random.Range(0, 1f));
        transform.localScale = new Vector3(originalScale, originalScale, 1) * .3f / transform.parent.localScale.x;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        if (dead)
        {
            Vector2 pos = transform.position;
            pos += vel * Time.deltaTime;
            vel = vel.normalized * (vel.magnitude * (1 - dieDrag * vel.magnitude * Time.deltaTime));
            transform.position = pos;
            if(vel.magnitude < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 灭火
    /// </summary>
    public void Quench(bool skill, Vector2 dir)
    {
        float shake = Random.Range(-shakeRange, shakeRange);
        Vector2 pos = originalPosition;
        pos += shake * dir;
        Vector3 localPos = pos;
        localPos.z = originalPosition.z;
        spriteTransform.localPosition = localPos;
        getHit = true;


        value -= Time.deltaTime * (skill ? 10 : 1);
        transform.localScale = new Vector3(value / quenchTime * scaleRange + minScale, value / quenchTime * scaleRange + minScale, 1) * .3f / transform.parent.localScale.x;
        if (value < 0)//火完全被灭时
        {
            vel = dir.normalized * dieSpeed;
            dead = true;
            fireObject.fireOn.Remove(gameObject);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (!lastGetHit && getHit)
        {
            if (smoke) smoke.Play();
            //mat.SetFloat("_Highlight", 1);
        }
        else if (lastGetHit && !getHit)
        {
            if (smoke) smoke.Stop();
            //mat.SetFloat("_Highlight", 0);
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
