using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Vector2 rotationRange;
    public List<Color> colorRange;
    public Transform targetPos;

    private bool startMove = false;

    public int MyScore
    {
        get
        {
            return int.Parse(GetComponent<Text>().text);
        }

        set
        {
            GetComponent<Text>().text = value.ToString();
        }
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(rotationRange.x, rotationRange.y));
        GetComponent<Text>().color = colorRange[Random.Range(0, colorRange.Count)];
    }

    private void Update()
    {
        if (startMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos.position, .1f);
            if(Vector3.Distance(transform.position,targetPos.position) < 10f)
            {
                Score.instance.score += MyScore;
                Destroy(gameObject);
            }
        }
    }

    public void StartMove() => startMove = true;
}
