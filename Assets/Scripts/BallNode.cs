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

        var up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        var down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        var left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        var right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        var mouseLeft = Input.GetMouseButtonDown(0);

        // select right key depending on direction

        if (up)
        {
            if (IsUp() && ball.CanJump())
            {
                print("jump up");
                ball.Jump();

                Invoke("JumpUp", .1f);
            }
            else
            {
                print("run upside");
                ball.Flip();
                Invoke("GoUp", .1f);
            }
        }
        else if (down)
        {
            if (IsDown() && ball.CanJump())
            {
                print("jump down");
                ball.Jump();

                Invoke("JumpDown", .1f);
            }
            else
            {
                print("run downside");
                ball.Flip();
                Invoke("GoDown", .1f);
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

        if (OnDie != null)
            OnDie();
    }

    public void Fire()
    {
        Bolt bolt = Instantiate(boltPrefab, transform.parent, true).GetComponent<Bolt>();
        Vector3 newPos = new Vector3(transform.position.x + .5f, transform.position.y + .1f, transform.position.z);
        bolt.transform.position =  newPos;

        bolt.transform.rotation = transform.rotation;
    }

    void JumpUp()
    {
        if (IsHorizontalMove())
            transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
        else
            transform.position = new Vector3(ball.GetNextZone().GetRoadX(), transform.position.y, transform.position.z);

        transform.localScale = new Vector3(1, -1, 1);
        ball.Swap();
    }

    void JumpDown()
    {
        if (IsHorizontalMove())
            transform.position = new Vector3(transform.position.x, ball.GetNextZone().GetRoadY(), transform.position.z);
        else
            transform.position = new Vector3(ball.GetNextZone().GetRoadX(), transform.position.y, transform.position.z);

        transform.localScale = new Vector3(1, 1, 1);
        ball.Swap();
    }

    void GoUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void GoDown()
    {
        transform.localScale = new Vector3(1, -1, 1);
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
