using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public int otherModifiers;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = ((int)(GameManager.GameInstance.player.transform.position.x) + otherModifiers + 4);
        scoreText.text = score + "";
    }
}
