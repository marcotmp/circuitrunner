using System;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallNode ballNode;
    private RoadZone otherRoadZone;
    private GameObject otherRoad;
    private RoadZone currentRoadZone;
    private GameObject currentRoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        otherRoadZone = collision.gameObject.GetComponent<RoadZone>();
        if (otherRoadZone != null)
        {
            otherRoad = otherRoadZone.road;

            // set object to current road if no road is set
            if (currentRoadZone == null)
            {
                currentRoadZone = otherRoadZone;
                currentRoad = otherRoad;
            }

            if (otherRoad.GetInstanceID() == currentRoad.GetInstanceID())
            {
                otherRoad = null;
                otherRoadZone = null;
            }
        }
    }

    public void Swap()
    {
        var tmpCurrentRoadZone = currentRoadZone;
        currentRoadZone = otherRoadZone;
        otherRoadZone = tmpCurrentRoadZone;

        currentRoad = currentRoadZone.road;
        otherRoad = otherRoadZone.road;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (otherRoadZone != null)
        {
            if (collision.gameObject.GetInstanceID() == otherRoadZone.GetInstanceID())
            {
                otherRoad = null;
                otherRoadZone = null;
            }
        }       
    }

    public bool CanJumpUp()
    {
        if (otherRoadZone != null)
        {
            return otherRoadZone.gameObject.name == "down";
        }
        return false;
    }

    public bool CanJumpDown()
    {
        if (otherRoadZone != null)
        {
            return otherRoadZone.gameObject.name == "up";
        }
        return false;
    }

    public RoadZone GetNextZone()
    {
        return otherRoadZone; //nextZoneGO.GetComponent<RoadZone>();
    }

    public void Hit()
    {
        ballNode.Hit();
    }
}
