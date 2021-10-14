using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPercentage : MonoBehaviour
{
    public float widthPercentage;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 tempPosition = transform.position;
        tempPosition.x = widthPercentage * Screen.width;
        transform.position = tempPosition;
    }
}
