using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBckg : MonoBehaviour
{
    public Camera view;
    public float parallaxVal;
    Vector2 length;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relPos = view.transform.position * parallaxVal;
        Vector3 dist = view.transform.position - relPos;

        if (dist.x > startPos.x + length.x)
        {
            startPos.x += length.x;
        }
        if (dist.x < startPos.x - length.x)
        {
            startPos.x -= length.x;
        }

        relPos.z = startPos.z;
        transform.position = startPos + relPos;
    }
}
