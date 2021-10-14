using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomScreenIndicator : MonoBehaviour
{

    public Text text;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameInstance.player.transform.position.y < 0)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
