using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("fire"))
        {
            Debug.Log("sd");
            Destroy(other.transform.parent.gameObject);
        }
    }
}
