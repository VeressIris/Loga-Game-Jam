using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float length;
    private float startPosX;
    [SerializeField] private GameObject cam;
    [SerializeField] private float effectStrength;

    void Start()
    {
        startPosX = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float dist = cam.transform.position.x * effectStrength;
        float temp = cam.transform.position.x * (1 - effectStrength);
        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if (temp < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
