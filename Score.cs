using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    int score = 0;
    private Text text;

     void Start()
    {
        text = GetComponent<Text>();   
    }
    public void ScoreGained(int points) {
        score += points;
        text.text = score.ToString();
    }
     void Reseet()
    {
        score = 0;
        text.text = score.ToString();
    }
}
   
  
