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

    public float deltaAngle = 80.0f;
    private float rotateAmount = 0;
    public float startAngle;
    public float actualAngle;
    public float finalAngle;

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


        finalAngle = startAngle + rotateAmount;
        //deltaAngle = (finalAngle - startAngle);
        var sign = finalAngle - startAngle;
        if (sign > 0)
            deltaAngle = Mathf.Abs(deltaAngle);
        else
            deltaAngle = Mathf.Abs(deltaAngle) * -1;

        actualAngle += deltaAngle * currentSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, actualAngle);
        if (deltaAngle > 0 && actualAngle > finalAngle || deltaAngle < 0 && actualAngle < finalAngle)
        {
            finalAngle = Clamp(finalAngle);
            transform.rotation = Quaternion.Euler(0, 0, finalAngle);
            startAngle = finalAngle;
            rotateAmount = 0;
            actualAngle = (int)Clamp(finalAngle);
        }

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

    public void RotateAmount(float angle)
    {
        rotateAmount = angle;
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

    public float GetAngle()
    {
        return actualAngle;
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

    private float Clamp(float angle)
    {
        if (angle >= 360)
            return angle - 360;

        if (angle < 0)
            return angle + 360;

        return angle;
    }

}
