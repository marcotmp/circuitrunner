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
        transform.position += transform.right * currentSpeed * Time.deltaTime;

        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var mouseLeft = Input.GetMouseButtonDown(0);

        // select right key depending on direction

        if (up)
        {
            if (IsUp() && ball.CanJump())
            {
                print("jump up");

                if (IsHorizontalMove())
                    transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
                else
                    transform.position = new Vector3(ball.GetNextZone().GetRoadX(), transform.position.y, transform.position.z);

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
        {
            if (IsDown() && ball.CanJump())
            {
                print("jump down");

                if (IsHorizontalMove())
                    transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
                else
                    transform.position = new Vector3(ball.GetNextZone().GetRoadX(), transform.position.y, transform.position.z);

                transform.localScale = new Vector3(1, 1, 1);
                ball.Swap();
            }
            else
            {
                print("run downside");
                transform.localScale = new Vector3(1, -1, 1);
            }
        }

        if (mouseLeft)
            Fire();
    }

    private bool IsHorizontalMove()
    {
        return transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 128;
    }

    private bool IsVerticalMove()
    {
        return transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 128;
    }

    public void ChangeDirection()
    {
        direction = Direction.DOWN;
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

    private bool IsUp()
    {
        return transform.localScale.y > 0;
    }

    private bool IsDown()
    {
        return transform.localScale.y < 0;
    }

}
