using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapHandler : MonoBehaviour
{
    public GameObject shopInstance;

    public void Shop()
    {      
        shopInstance.SetActive(true);
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("Shop");
    }

    public void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("StartGame");
            GameObject.Find("Canvas").transform.Find("GUIInGame").gameObject.SetActive(true);
            GameManager.GameInstance.StartGame();
            Destroy(this);
        }
    }
}
