using System;
using UnityEngine;

public class Ball : MonoBehaviour {
    public BallNode ballNode;
    private RoadZone otherRoadZone;
    private RoadZone currentRoadZone;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tmpRoadZone = collision.gameObject.GetComponent<RoadZone>();
        if (tmpRoadZone != null)
        {
            print("roadZone name = " + tmpRoadZone.gameObject.name);

            // set object to current road if no road is set
            if (currentRoadZone == null)
            {
                currentRoadZone = tmpRoadZone;
            }

            if (tmpRoadZone.IsSameRoad(currentRoadZone))
            {
                currentRoadZone = tmpRoadZone;
            }
            else
            {
                otherRoadZone = tmpRoadZone;
            }
            //print(currentRoadZone + " " + otherRoadZone);
            print("current: " + currentRoadZone.gameObject.name + ", other: " + (otherRoadZone != null ? otherRoadZone.gameObject.name : ""));
        }
    }

    public void Swap()
    {
        var tmpCurrentRoadZone = currentRoadZone;
        currentRoadZone = otherRoadZone;
        otherRoadZone = tmpCurrentRoadZone;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (otherRoadZone != null)
        {
            if (collision.gameObject.GetInstanceID() == otherRoadZone.GetInstanceID())
            {
                otherRoadZone = null;
            }
        }       
    }

    public bool CanJump()
    {
        return otherRoadZone != null;
    }
    
    public RoadZone GetNextZone()
    {
        return otherRoadZone;
    }

    public void Hit()
    {
        ballNode.Hit();
    }
}
