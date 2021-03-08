using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;
    public int Score
    {
        get{return score;}
        set
        {
            // set score min to zero
            score = value < 0 ? 0 : value;
            // update score text
            scoreText.text = "Score: " + score;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }
}
