using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    public float minX;
    public float maxX;

    public int minDots;
    public int maxDots;

    public float absoluteMin;
    public float absoluteMax;

    public float minY;
    public float maxY;

    public float currentXOffset;

    public GameObject point;
    public GameObject wall;
    public GameObject glow;

    public float coinLocation;
    float seed;

    public void Awake()
    {
        seed = Random.Range(0f, 1500f);
    }

    public void SetUpLevelPiece()
    {
        int randomDots = Random.Range(minDots, maxDots);
        float x;
        float y;

        for (int i = 0; i < randomDots; i++)
        {
            //float y = Mathf.Clamp(Random.Range(minY, maxY) * Mathf.Pow(-1, Random.Range(0, 2)) + currentYOffset, absoluteMin, absoluteMax);
            GameObject temp = Instantiate(point);

            temp.transform.parent = transform;
            x = Random.Range(minX, maxX) + currentXOffset;
            currentXOffset = x;
            y = Mathf.Clamp((Mathf.PerlinNoise(currentXOffset, seed) * (absoluteMax - absoluteMin)) * 3, absoluteMin, absoluteMax);

            temp.transform.localPosition = new Vector3(x, y);
        }
        x = Random.Range(minX, maxX) + currentXOffset + 2f;
        y = Mathf.Clamp((Mathf.PerlinNoise(currentXOffset, seed) * (absoluteMax - absoluteMin)) * 3, absoluteMin, absoluteMax);
        currentXOffset = x;

        GameObject tempWall = Instantiate(wall);
        tempWall.transform.parent = transform;
        tempWall.transform.localPosition = new Vector3(x, wall.transform.position.y);
        tempWall.GetComponent<StraightWall>().SetCoinLocation(y);

        GameObject glowCoin = Instantiate(glow);
        glowCoin.transform.parent = transform;
        glowCoin.transform.localPosition = new Vector3(x, y);
        coinLocation = y;
    }

    public void HandleFirst()
    {
        GameObject temp = Instantiate(point);

        temp.transform.parent = transform;
        float x = Random.Range(2, 2.2f);
        float y = Mathf.Clamp((Mathf.PerlinNoise(currentXOffset, seed) * (absoluteMax - absoluteMin)) * 3, absoluteMin, absoluteMax);

        temp.transform.localPosition = new Vector3(x, y);
    }
}
