using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public Vector2 rotationRange;
    public Vector2 positionRange;

    public GameObject firePrefab;
    private Shine shine;
    [HideInInspector] public List<GameObject> fireOn = new List<GameObject>();

    public BoxCollider2D[] fireGenerateRange;

    private Animator animator;
    private bool flied = false;
    private bool startToFly = false;

    public GameObject scoreObject;
    public Vector2 scoreRange;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        shine = GetComponentInChildren<Shine>();
    }


    private void Start()
    {
        GenerateFire();

        float angle = transform.rotation.eulerAngles.z;
        angle += Random.Range(rotationRange.x, rotationRange.y);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 position = transform.position;
        position.x += Random.Range(positionRange.x, positionRange.y);
        transform.position = position;


        //for (var i = 0; i < Random.Range(fireRange.x, fireRange.y + 1); i++)
        //{
        //    GameObject fire = Instantiate(firePrefab,
        //    transform.position, transform.rotation);
        //    fire.transform.parent = transform;
        //    fire.transform.localPosition = new Vector2(Random.Range(-1.0f, 1.0f) * firePosRange.x, Random.Range(-1.0f, 1.0f) * firePosRange.y);
        //    fireOn.Add(fire);
        //}
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!flied && fireOn.Count == 0)
        {
            //todo:翻身变可爱
            animator.SetTrigger("Fly");
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            ScoreText score = Instantiate(scoreObject, position, Quaternion.identity,scoreObject.transform.parent).GetComponent<ScoreText>();
            score.MyScore = Mathf.RoundToInt(Random.Range(scoreRange.x, scoreRange.y));
            score.gameObject.SetActive(true);
            flied = true;
        }

        if (startToFly)
        {
            transform.position += new Vector3(0, Time.deltaTime * 15, 0);
        }
    }

    public void AnimEnd()
    {
        startToFly = true;
    }

    private void GenerateFire()
    {
        for (int i = 0; i < fireGenerateRange.Length; i++)
        {
            float x_min = fireGenerateRange[i].bounds.min.x;
            float x_max = fireGenerateRange[i].bounds.max.x;
            float y_min = fireGenerateRange[i].bounds.min.y;
            float y_max = fireGenerateRange[i].bounds.max.y;
            Vector2 position = new Vector2(Random.Range(x_min, x_max), Random.Range(y_min, y_max));
            GameObject fire = Instantiate(firePrefab, position, transform.rotation, transform);
            fireOn.Add(fire);
            fire.GetComponent<Fire>().fireObject = this;
        }
    }

    public void Shine() { if (shine) shine.StartShine(); }
}
