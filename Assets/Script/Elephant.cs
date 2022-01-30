using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{
    public Transform Nose;//鼻子
    public float maxDis;//最大鼻子长度
    public GameObject progressBar;//进度条
    public float skillTime;//技能蓄力时间
    public Transform Beam;//水柱
    public Transform Beam2;//水柱
    public Transform hitEffect;
    public float beamOffset;
    public Vector2 MaxAndMinWidth;//水柱宽度范围

    public Transform startPhysic;
    public Transform endPhysic;
    public Sprite[] elephantSprite;
    public SpriteRenderer eleRenderer;

    private Vector3 fountainPos;//喷泉位置
    private Vector3 mousePos;//鼠标位置
    private Vector3 Dir;//鼻子方向
    private bool fountain;//鼻子在喷泉
    private bool skill;//技能
    private float skillValue;//技能蓄力值
    private ProgressBar PB;
    private bool skillStart = true;


    public GameObject skillCircle;
    public Transform skillSprite;
    public Animator elephantAnim;

    private Animator fountainAnim;
    private bool animPlayed = false;

    private AudioSource source;

    void Awake()
    {
        PB = progressBar.GetComponent<ProgressBar>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        NoseMove();
        if(!skill) FireTrigger();
        Beam.position = endPhysic.position;
        Beam.rotation = Quaternion.Euler(0, 0, endPhysic.eulerAngles.z);
        skillSprite.position = endPhysic.position;
        skillSprite.rotation = Quaternion.Euler(0, 0,endPhysic.eulerAngles.z);
    }
    void NoseMove()
    {
        float speedRate = 1;
        bool beamActive = true;
        float animSpeed = 1;

        if (Input.GetMouseButton(0) && fountain && (fountainPos - startPhysic.position).magnitude < maxDis)//点击喷泉
        {
            skillValue += Time.deltaTime;
            skillValue = Mathf.Clamp(skillValue, 0, skillTime);
            
            mousePos = fountainPos;
            if (skillValue / skillTime > .33f)
            {
                skill = true;
                animPlayed = false;
            }


            progressBar.SetActive(true);
            PB.barKind = ProgressBarKind.discrete;
            PB.value = skillValue / skillTime * 100;
            speedRate = 0;

            beamActive = false;
            skillSprite.gameObject.SetActive(false);
            skillCircle.SetActive(false);

            animSpeed = 0;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);

            if (skill)
            {
                progressBar.SetActive(true);
                PB.barKind = ProgressBarKind.continuous;
                PB.value = skillValue / skillTime * 100;
                speedRate = 10;
                skillCircle.SetActive(true);
                skillSprite.gameObject.SetActive(true);
                beamActive = false;
                mousePos = startPhysic.position + new Vector3(.3f, .2f, 0);
                animSpeed = 3;

                if (!animPlayed)
                {
                    fountainAnim.SetBool("Used", true);
                    animPlayed = true;
                    source.Play();
                }
            }
            else
            {
                progressBar.SetActive(false);
                skillCircle.SetActive(false);
                skillSprite.gameObject.SetActive(false);
            }




            if (skillValue > 0)
                skillValue -= Time.deltaTime;
            else
                skill = false;
        }

        Planet.SpeedRate = speedRate;
        Beam.gameObject.SetActive(beamActive);
        elephantAnim.speed = animSpeed;
        

        eleRenderer.sprite = elephantSprite[(int)((skillValue / skillTime) * 3)];

        Dir = mousePos - startPhysic.position;
        float dist = Dir.magnitude;
        dist = Mathf.Clamp(dist, -maxDis, maxDis);
        Vector3 endPoint = startPhysic.position + dist * Dir.normalized;
        endPhysic.GetComponent<Rigidbody2D>().MovePosition(endPoint);

    }
    void FireTrigger()
    {
        bool hitFire = false;
        RaycastHit2D hit = Physics2D.Raycast(startPhysic.position, Dir, Mathf.Infinity, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            GameObject gameObj = hit.collider.gameObject;
            Debug.DrawLine(startPhysic.position, hit.point, Color.green, 2.5f);
            if (gameObj.CompareTag("fire"))
            {
                gameObj.GetComponent<Fire>().Quench(skill, -hit.normal);
                Vector2 size = new Vector2((new Vector3(hit.point.x, hit.point.y, 0) - endPhysic.position).magnitude * beamOffset,
                     skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
                Beam.GetComponent<SpriteRenderer>().size = size;
                Beam2.GetComponent<SpriteRenderer>().size = size;
                hitFire = true;
                hitEffect.position = hit.point;
            }
        }

        if (!hitFire)
        {
            Beam.GetComponent<SpriteRenderer>().size = new Vector2(27f, MaxAndMinWidth.x);
            Beam2.GetComponent<SpriteRenderer>().size = new Vector2(27f, MaxAndMinWidth.x);
        }

        hitEffect.gameObject.SetActive(hitFire);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("fountain"))
        {
            fountain = true;
            fountainPos = col.transform.position;
            fountainAnim = col.GetComponent<Animator>();
            fountainAnim.SetBool("InRange", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("fountain"))
        {
            fountain = false;
            fountainAnim.SetBool("InRange", false);
            fountainAnim = null;
            col.GetComponent<Fountain>().DestroyFountain();
            //Beam2.GetComponent<SpriteRenderer>().size = new Vector2(27f, skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
            //Beam.GetComponent<SpriteRenderer>().size = new Vector2(27f, skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
        }
    }
}
