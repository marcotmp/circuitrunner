using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNode : MonoBehaviour, CameraTargetable
{
    public float speed = 1;
    public Direction direction;
    public float movementAngle = 0;
    public Ball ball;
    public GameObject boltPrefab;

    public Action OnDie { get; internal set; }

    private float currentSpeed;

    // Use this for initialization
    void Start()
    {
        currentSpeed = speed;
        transform.rotation = Quaternion.Euler(
            new Vector3(transform.rotation.x, transform.rotation.y, (float)direction)
        );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var mouseLeft = Input.GetMouseButtonDown(0);

        // select right key depending on direction

        if (up)
        {
            if (ball.CanJumpUp())
            {
                print("jump up");
                transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
                transform.localScale = new Vector3(1, -1, 1);
                ball.Swap();
            }
            else
            {
                print("run upside");
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (down)
            transform.localScale = new Vector3(1, -1, 1);

        if (mouseLeft)
            Fire();
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetVelocity()
    {
        return speed;
        if (ball.CanJumpDown())
        {
            print("jump up");
            transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
            ball.Swap();
        }
        else
        {
            print("run upside");
            transform.localScale = new Vector3(1, -1, 1);
        }
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

    public void Fire()
    {
        Bolt bolt = Instantiate(boltPrefab, transform.parent, true).GetComponent<Bolt>();
        bolt.transform.position = transform.position;
        bolt.transform.rotation = transform.rotation;
    }
}
