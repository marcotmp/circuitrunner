using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    public BallNode ballNode;
    public RoadZone otherRoadZone;
    public RoadZone currentRoadZone;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tmpRoadZone = collision.gameObject.GetComponent<RoadZone>();
        if (tmpRoadZone != null)
        {
            //print("roadZone name = " + tmpRoadZone.gameObject.name);

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
        var roadzone = collision.gameObject.GetComponent<RoadZone>();
        if (roadzone)
        {
            var name = roadzone.road.gameObject.name;
            print("exit " + name);

            if (otherRoadZone != null && name == otherRoadZone.road.gameObject.name)
                otherRoadZone = null;
            if (currentRoadZone != null && name == currentRoadZone.road.gameObject.name)
                currentRoadZone = null;
        }


        //var collisionID = collision.gameObject.GetInstanceID();

        //if (otherRoadZone != null && otherRoadZone.GetInstanceID() == collision.gameObject.GetInstanceID())
        //    otherRoadZone = null;

        //var currentID = currentRoadZone.GetInstanceID();
        //if (currentRoadZone != null && currentID == collisionID)
        //    currentRoadZone = null;

    }

    public float GetAngle()
    {
        return ballNode.GetAngle();
    }

    public bool CanJump()
    {
        return otherRoadZone != null;
    }
    
    public RoadZone GetNextZone()
    {
        return otherRoadZone;
    }

    public void RotateTo(float angle)
    {
        ballNode.RotateAmount(angle);
    }

    public void Hit()
    {
        ballNode.Hit();
    }

    public Direction GetDirection()
    {
        return ballNode.GetDirection();
    }
}
