using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObject : MonoBehaviour
{
    public Vector3 normalSize;
    public Vector3 expandedSize;
    public bool locked;
    public Shader lockedShader;

    private bool toggle;

    public string objectName;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotateAngles = transform.localEulerAngles;
        rotateAngles.y += 50 * Time.deltaTime;
        transform.localEulerAngles = rotateAngles;

        if (toggle)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, expandedSize, Time.deltaTime * 6);
        } else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, normalSize, Time.deltaTime * 6);

        }
    }

    public void Toggle()
    {
        toggle = !toggle;
    }
}
