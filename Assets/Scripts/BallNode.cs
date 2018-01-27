using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNode : MonoBehaviour {
    public float speed = 1;
    public Ball ball;

    public Action OnDie { get; internal set; }

    private float currentSpeed;

    // Use this for initialization
	void Start () {
        currentSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime;

        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        // select right key depending on direction

        if (up)
        {
            if (ball.nextZone == "down")
            {
                var roadZone = ball.nextZoneGO.GetComponent<RoadZone>();
                transform.position = new Vector3(transform.position.x, roadZone.road.transform.position.y, transform.position.z);
                transform.localScale = new Vector3(1, -1, 1);
                ball.nextZone = "";
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (down)
            transform.localScale = new Vector3(1, -1, 1);
	}

    public void Hit()
    {
        Die();
    }

    public void Die()
    {
        currentSpeed = 0;
        ball.gameObject.SetActive(false);
        OnDie();
    }
}
