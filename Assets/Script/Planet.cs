using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public GameObject[] fireObjectPrefab;//着火物体预制体
    public float cInterVal;//创造物体的间隔

    private int index = 0;
    private float timeVal;//计时器
    [HideInInspector] public int createTime;//创造物体的次数

    private void Awake()
    {
        index = Random.Range(0, fireObjectPrefab.Length);
    }

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
        Transform FO = Instantiate(fireObjectPrefab[index], fireObjectPrefab[index].transform.position, fireObjectPrefab[index].transform.rotation, transform).transform;
        
        int randomIndex = Random.Range(0, fireObjectPrefab.Length);
        int count = 0;
        while (randomIndex == index && count <= 3)  
        {
            count++;
            randomIndex = Random.Range(0, fireObjectPrefab.Length);
        }

        index = randomIndex;

        FO.gameObject.SetActive(true);

        //FO.position = new Vector3(Random.Range(CreateRangeStart.position.x, CreateRangeEnd.position.x),
        //    Random.Range(CreateRangeStart.position.y, CreateRangeEnd.position.y));
    }
}
