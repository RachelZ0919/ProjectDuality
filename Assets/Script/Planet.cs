﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public Transform CreateRangeStart;//生成位置范围
    public Transform CreateRangeEnd;//生成位置范围
    public GameObject fireObjectPrefab;//着火物体预制体
    public float cInterVal;//创造物体的间隔


    private float timeVal;//计时器
    [HideInInspector] public int createTime;//创造物体的次数

    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        timeVal += Time.deltaTime;
        if (timeVal > cInterVal)
        {
            CreateFireObject();
            createTime++;
            timeVal = 0;
        }
    }

    void CreateFireObject()
    {
        Transform FO = Instantiate(fireObjectPrefab).transform;
        FO.parent = transform;
        FO.position = new Vector3(Random.Range(CreateRangeStart.position.x, CreateRangeEnd.position.x),
            Random.Range(CreateRangeStart.position.y, CreateRangeEnd.position.y));
    }
}