using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public void DestroyFountain()
    {
        StartCoroutine(DestroyNumerator());
    }

    private IEnumerator DestroyNumerator()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
