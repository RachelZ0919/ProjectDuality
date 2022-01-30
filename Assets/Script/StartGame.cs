using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    private bool start = false;

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void StartWhat()
    {
        if (start) return;
        start = true;
        GetComponent<Animator>().SetTrigger("Start");
    }
}
