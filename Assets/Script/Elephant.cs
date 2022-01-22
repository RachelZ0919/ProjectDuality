using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{
    public Transform startTr;//鼻根
    public Transform endTr;//鼻子末梢
    public Transform Nose;//鼻子
    public float maxDis;//最大鼻子长度
    public GameObject progressBar;//进度条
    public float skillTime;//技能蓄力时间
    public Transform Beam;//水柱
    public Transform Beam2;//水柱
    public float beamOffset;
    public Vector2 MaxAndMinWidth;//水柱宽度范围


    private Vector3 fountainPos;//喷泉位置
    private Vector3 mousePos;//鼠标位置
    private Vector3 Dir;//鼻子方向
    private bool fountain;//鼻子在喷泉
    private bool skill;//技能
    private float skillValue;//技能蓄力值
    private ProgressBar PB;


    void Awake()
    {
        PB = progressBar.GetComponent<ProgressBar>();
    }
    void Update()
    {
        NoseMove();
        FireTrigger();
        Beam.position = endTr.position;
        Beam.rotation = Quaternion.Euler(0, 0, Nose.eulerAngles.z - 90);
    }
    void NoseMove()
    {
        if (Input.GetMouseButton(0) && fountain && (fountainPos - startTr.position).magnitude < maxDis)//点击喷泉
        {
            skillValue += Time.deltaTime;
            mousePos = fountainPos;
            if (skillValue / skillTime > 1)
                skill = true;

            progressBar.SetActive(true);
            PB.barKind = ProgressBarKind.discrete;
            PB.value = skillValue / skillTime * 100;
        }
        else
        {
            if (skill)
            {
                progressBar.SetActive(true);
                PB.barKind = ProgressBarKind.continuous;
                PB.value = skillValue / skillTime * 100;
            }
            else
                progressBar.SetActive(false);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);

            if (skillValue > 0)
                skillValue -= Time.deltaTime;
            else
                skill = false;
        }

        // transform.LookAt(mousePos, Vector3.up);
        Dir = mousePos - startTr.position;
        Nose.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg);
        // Debug.DrawLine(mousePos, startTr.position, Color.green, 2.5f);
        if (Dir.magnitude < maxDis)
            Nose.localScale = new Vector3(1, Dir.magnitude, 1);
        else
            Nose.localScale = new Vector3(1, maxDis, 1);
    }
    void FireTrigger()
    {
        RaycastHit2D hit = Physics2D.Raycast(startTr.position, Dir);
        if (hit.collider != null)
        {
            GameObject gameObj = hit.collider.gameObject;
            Debug.DrawLine(startTr.position, hit.point, Color.green, 2.5f);
            Debug.Log(gameObj.name);
            if (gameObj.CompareTag("fire"))
            {
                gameObj.GetComponent<Fire>().Quench(skill);
                Vector2 size = new Vector2((new Vector3(hit.point.x, hit.point.y, 0) - endTr.position).magnitude * beamOffset,
                     skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
                Beam.GetComponent<SpriteRenderer>().size = size;
                Beam2.GetComponent<SpriteRenderer>().size = size;
            }
            else if (gameObj.CompareTag("fountain"))
            {
                fountain = true;
                fountainPos = gameObj.transform.position;
            }
        }
        else
        {
            fountain = false;
            Beam2.GetComponent<SpriteRenderer>().size = new Vector2(27f, skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
            Beam.GetComponent<SpriteRenderer>().size = new Vector2(27f, skill ? MaxAndMinWidth.y : MaxAndMinWidth.x);
        }
    }
}
