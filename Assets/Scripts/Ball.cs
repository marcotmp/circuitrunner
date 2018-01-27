using System;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallNode ballNode;
    public string currentZone = "";
    public string nextZone = "";

    public GameObject currentZoneGO;
    public GameObject nextZoneGO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        var zone = collision.gameObject.name;

        if (string.IsNullOrEmpty(currentZone))
        {
            currentZone = zone;
            currentZoneGO = collision.gameObject;
        }

        if (nextZone != zone && currentZone != zone)
        {
            nextZone = zone;
            nextZoneGO = collision.gameObject;
        }
    }

    public void Hit()
    {
        ballNode.Hit();
    }
}
