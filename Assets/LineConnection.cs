using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection : MonoBehaviour
{
    public Transform Point;
    public Transform Player;

    private LineRenderer line;

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, Point.position);
        line.SetPosition(1, Player.position);
    }

    public void LineEndPoint(Transform point)
    {
        Point = point;
    }
}
