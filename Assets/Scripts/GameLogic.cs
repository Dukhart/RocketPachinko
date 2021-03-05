using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public int score = 0;
    public GameObject textObj;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
    }
    public void UpdateScore ()
    {
        Text text = textObj.GetComponent<Text>();
        text.text = score.ToString();
    }
}
