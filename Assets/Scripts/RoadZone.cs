using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadZone : MonoBehaviour {

    public GameObject road;
    private Color color;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    public float GetRoadY()
    {
        return road.transform.position.y;
    }

    public float GetRoadX()
    {
        return road.transform.position.x;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            //var color = Color.red;
            //color.a = 0.5f;
            //spriteRenderer.color = color;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            spriteRenderer.color = color;
        }
    }

    public bool IsSameRoad(RoadZone otherRoad)
    {
        return (road.GetInstanceID() == otherRoad.road.GetInstanceID());
    }
}
