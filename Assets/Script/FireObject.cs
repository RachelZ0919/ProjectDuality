using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public Vector2 fireRange;//火焰数量范围
    public Vector2 firePosRange;//火焰位置范围
    public GameObject firePrefab;
    public List<GameObject> fireOn;
    private void Start()
    {
        for (var i = 0; i < Random.Range(fireRange.x, fireRange.y + 1); i++)
        {
            GameObject fire = Instantiate(firePrefab,
            transform.position, transform.rotation);
            fire.transform.parent = transform;
            fire.transform.localPosition = new Vector2(Random.Range(-1.0f, 1.0f) * firePosRange.x, Random.Range(-1.0f, 1.0f) * firePosRange.y);
            fireOn.Add(fire);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (fireOn.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
