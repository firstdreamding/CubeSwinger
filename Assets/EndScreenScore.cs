 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScore : MonoBehaviour
{
    public ScoreUI score;

    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Text>().text = "Score: " + score.score;
        score.transform.parent.gameObject.SetActive(false);
    }
}
