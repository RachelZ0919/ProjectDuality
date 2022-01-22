using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseNode : MonoBehaviour
{
    public Transform targetNode;
    public bool isReverse = false;
    private float originAngle;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        originAngle = rigid.rotation;
    }

    private void Update()
    {
        Vector3 dir = targetNode.transform.position - transform.position;
        dir = dir.normalized;
        if (isReverse) dir *= -1;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.SetRotation(originAngle + angle);
    }
}
