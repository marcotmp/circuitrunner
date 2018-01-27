using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallNode ballNode;

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void Hit()
    {
        ballNode.Hit();
    }
}
