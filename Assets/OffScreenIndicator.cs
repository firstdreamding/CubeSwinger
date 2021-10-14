using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    Vector3 pos;
    public Text offText;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.height >= Camera.main.WorldToScreenPoint(GameManager.GameInstance.currentModel.transform.position).y) {
            offText.enabled = false;
        } else
        {
            offText.enabled = true;
            offText.fontSize = (int)Mathf.Min((150 / ((Camera.main.WorldToScreenPoint(GameManager.GameInstance.currentModel.transform.position).y - Screen.height) / 100)), 100f);
        }
    }
}
