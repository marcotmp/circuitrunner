using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNode : MonoBehaviour, CameraTargetable
{
    public float speed = 1;
    public Direction direction;
    public float movementAngle = 0;

    // Use this for initialization
    void Start () {
        direction = Direction.RIGHT;

        //Invoke("ChangeDirection", 5f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.right * speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (int)direction));

        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);

        if (up)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (down)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
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
}
