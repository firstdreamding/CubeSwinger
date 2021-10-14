using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    public Text scoreText;
    public int otherModifiers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Speed: " + ((int)(GameManager.GameInstance.player.GetComponent<Rigidbody>().velocity.x));
    }
}
