using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadZone : MonoBehaviour {

    public GameObject road;

    public float GetRoadY()
    {
        return road.transform.position.y;
    }
}
