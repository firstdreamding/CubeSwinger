using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightWall : MonoBehaviour
{
    public GameObject wallExplosion;
    public float size;
    public float coinLocation;

    Rigidbody playerRigid;
    BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GameManager.GameInstance.player.GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRigid.velocity.x > 8)
        {
            bc.isTrigger = true;
        } else {
            bc.isTrigger = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameManager.GameInstance.OnWallCompleted();
        GetComponent<BoxCollider>().enabled = false;

        if (Mathf.Abs(GameManager.GameInstance.player.transform.position.y - coinLocation) < size)
        {
            PlayerPrefs.SetInt("CoinsAmount", PlayerPrefs.GetInt("CoinsAmount") + 1);
            GameManager.GameInstance.UpdateCoin();
        }
    }

    public void SetCoinLocation(float coin)
    {
        coinLocation = coin;
    }
}
