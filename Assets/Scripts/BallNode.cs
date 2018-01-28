using System;
using UnityEngine;

public enum BallStartAngle { RIGHT = 0, LEFT = 180, UP = 90, DOWN = 270 };

public class BallNode : MonoBehaviour
{
    public enum BallState
    {
        Idle,
        Running,
        Rotating,
        Dead,
        Win
    };

    public float speed = 1;
    public Ball ball;
    public GameObject boltPrefab;

    public Action OnDie { get; internal set; }
    public Action OnWin { get; internal set; }

    private BallState state;

    private float currentSpeed;

    public float deltaAngle = 80.0f;
    private float rotateAmount = 0;
    public float startAngle;
    public float actualAngle;
    public float finalAngle;

    float jumpTime = .05f;
    float flipTime = .05f;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * currentSpeed * Time.deltaTime;

        if (rotateAmount != 0)
        {
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
                state = BallState.Running;
            }
        }

        if (state != BallState.Running) return;

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

                Invoke("JumpUp", jumpTime);
            }
            else
            {
                print("run upside");
                ball.Flip();
                Invoke("GoUp", flipTime);
            }
        }
        else if (down)
        {
            if (IsDown() && ball.CanJump())
            {
                print("jump down");
                ball.Jump();

                Invoke("JumpDown", jumpTime);
            }
            else
            {
                print("run downside");
                ball.Flip();
                Invoke("GoDown", flipTime);
            }
        }

        if (mouseLeft)
            Fire();
    }

    public void StartRun()
    {
        currentSpeed = speed;
        state = BallState.Running;
    }

    public void RotateAmount(float angle)
    {
        rotateAmount = angle;
        state = BallState.Rotating;
    }

    private bool IsHorizontalMove()
    {
        return transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180;
    }

    private bool IsVerticalMove()
    {
        return transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 128;
    }

    public float GetAngle()
    {
        return actualAngle;
    }

    public void SetStartAngle(BallStartAngle startAngle)
    {
        this.startAngle = (float)startAngle;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetVelocity()
    {
        return speed;
    }


    public void Win()
    {
        currentSpeed = 0;

        state = BallState.Win;

        if (OnWin != null)
            OnWin();
    }

    public void Hit()
    {
        Die();
    }

    public void Die()
    {
        currentSpeed = 0;

        state = BallState.Dead;

        if (OnDie != null)
            OnDie();
    }

    public void Fire()
    {
        Bolt bolt = Instantiate(boltPrefab, transform.parent, true).GetComponent<Bolt>();
        Vector3 newPos;
        if (IsHorizontalMove())
            newPos = new Vector3(transform.position.x + .5f, transform.position.y + .1f, transform.position.z);
        else
            newPos = new Vector3(transform.position.x + .1f, transform.position.y + .5f, transform.position.z);

        bolt.transform.position = newPos;

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

    private float Clamp(float angle)
    {
        if (angle >= 360)
            return angle - 360;

        if (angle < 0)
            return angle + 360;

        return angle;
    }
}
