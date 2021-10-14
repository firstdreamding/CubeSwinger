using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public ScoreUI score;
    public Text text;
    // Start is called before the first frame update
    void OnEnable()
    {
        int highscore = PlayerPrefs.GetInt("Highscore");
        if (score.score > highscore)
        {
            highscore = score.score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        text.text = "Highscore: " + highscore;
    }
}
