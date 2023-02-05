using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float punchBack = 200;
    public float punchUp = 100;
    public float kickBack = 50;
    public float kickUp = 150;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void imHit(bool flipped, int attack)
    {
        if (flipped)
        {
            if (attack == 0)
            {
                rb2d.AddForce(new Vector2(-punchBack, punchUp));
            }
            else if (attack == 1)
            {
                rb2d.AddForce(new Vector2(-kickBack, kickUp));
            }
        }
        else
        {
            if (attack == 0)
            {
                rb2d.AddForce(new Vector2(punchBack, punchUp));
            }
            else if (attack == 1)
            {
                rb2d.AddForce(new Vector2(kickBack, kickUp));
            }
        }
        Debug.Log("OWOWOWOOWOWOWOWOOWOWOWOWOWOWOWWWW");
    }
}
