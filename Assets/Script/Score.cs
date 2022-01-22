using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int score
    {
        get
        {
            return int.Parse(GetComponent<Text>().text);
        }

        set
        {
            GetComponent<Text>().text = value.ToString();
        }
    }
    private int _score = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }
}
