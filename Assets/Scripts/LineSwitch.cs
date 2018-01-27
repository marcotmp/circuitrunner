using System.Collections.Generic;
using UnityEngine;

public class LineSwitch : MonoBehaviour {

    public enum LineSwitchAngle { ZERO=0, NINE=90, ONEEIGHT=180, TWOSEVEN=270 };
    public LineSwitchAngle startingAngle;
    
    // key - switch angle
    // value - ball angle
    Dictionary<float, bool> passageRules = new Dictionary<float, bool>()
    {
        { (float)LineSwitchAngle.ZERO, true },
        { (float)LineSwitchAngle.NINE, false },
        { (float)LineSwitchAngle.ONEEIGHT, true },
        { (float)LineSwitchAngle.TWOSEVEN, false }
    };

    Dictionary<float, LineSwitchAngle> entryAngles = new Dictionary<float, LineSwitchAngle>()
    {
        { 0, LineSwitchAngle.ONEEIGHT },
        { 180, LineSwitchAngle.ZERO },
        { 90, LineSwitchAngle.TWOSEVEN },
        { 270, LineSwitchAngle.NINE }
    };

    LineSwitchAngle currentAngle;
    List<LineSwitchAngle> switchOrder = new List<LineSwitchAngle>()
    {
        LineSwitchAngle.ZERO, LineSwitchAngle.NINE, LineSwitchAngle.ONEEIGHT, LineSwitchAngle.TWOSEVEN
    };
    int currentAngleIndex = 0;
    bool locked = false;

    // Use this for initialization
    void Start () {
        currentAngle = startingAngle;
        currentAngleIndex = switchOrder.IndexOf(currentAngle);
        UpdateAngle();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bolt projectile = collision.GetComponent<Bolt>();
        if (projectile != null)
        {
            if (!locked)
            {
                // signal projectile to destroy itself
                projectile.Hit();

                SwitchAngle();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            locked = true;
            float ballAngle = (float)ball.GetDirection();
            LineSwitchAngle entryAngle = entryAngles[ballAngle];
            float finalAngle = Mathf.Abs((float)entryAngle - (float)currentAngle);

            // the ball didn't enter from the right direction
            if (!passageRules[finalAngle])
            {
                ball.Hit();
            }
        }
    }

    void UpdateAngle()
    {
        transform.rotation = Quaternion.Euler(
            new Vector3(transform.rotation.x, transform.rotation.y, (float)currentAngle)
        );
    }

    void SwitchAngle()
    {
        currentAngleIndex = (currentAngleIndex + 1) % switchOrder.Count;
        currentAngle = switchOrder[currentAngleIndex];
        UpdateAngle();
    }
}
