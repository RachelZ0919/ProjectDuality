using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{
    public Transform startTr;
    // public Transform endTr;
    public float maxDis;
    private Vector3 mousePos;
    private Vector3 Dir;
    void Update()
    {
        NoseMove();
        FireTrigger();
    }
    void NoseMove()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        // transform.LookAt(mousePos, Vector3.up);
        Dir = mousePos - startTr.position;
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg);
        // Debug.DrawLine(mousePos, startTr.position, Color.green, 2.5f);
        if (Dir.magnitude < maxDis)
            transform.localScale = new Vector3(1, Dir.magnitude, 1);
        else
            transform.localScale = new Vector3(1, maxDis, 1);
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
                gameObj.GetComponent<FireObject>().Quench();
            }
        }
    }
}
