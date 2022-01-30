using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fire fire = collision.GetComponent<Fire>();
        if (fire)
        {
            Vector2 pos1 = collision.transform.position;
            Vector2 pos2 = transform.position;
            fire.Quench(true, (pos1 - pos2).normalized);
        }
    }
}
