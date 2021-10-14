using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    public bool attached;
    private ConfigurableJoint joint;

    private void OnEnable()
    {
        // Subcribe to events when object is enabled
        TouchManager.OnTouchDown += OnTouchDown;
        TouchManager.OnTouchUp += OnTouchUp;
    }

    private void OnDisable()
    {
        // Unsubcribe from events when object is disabled
        TouchManager.OnTouchDown -= OnTouchDown;
        TouchManager.OnTouchUp -= OnTouchUp;
    }

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void OnTouchDown(Touch eventData)
    {
        GameManager.GameInstance.Connect(gameObject);
    }

    private void OnTouchUp(Touch eventData)
    {
        GameManager.GameInstance.BreakConnection();
    }
}
