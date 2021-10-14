using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private Vector3 position;
    

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    void LateUpdate()
    {
        position.x = player.transform.position.x;
        transform.position = position;
    }
}
