using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float lerpFactor = 1.0f;

    void Update()
    {
        Vector3 centerPoint = Vector3.Lerp(player1.position, player2.position, 0.5f);
        transform.position = Vector3.Lerp(transform.position, centerPoint, lerpFactor);
        transform.position += new Vector3(0, 0, -10);


    }
}
