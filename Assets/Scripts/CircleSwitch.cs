using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSwitch : MonoBehaviour {

    public enum CircleSwitchAngle { ZERO = 0, NINE = 90, ONEEIGHT = 180, TWOSEVEN = 270 };
    public CircleSwitchAngle startingAngle;

    // key - switch angle
    // value - ball angle
    Dictionary<float, bool> passageRules = new Dictionary<float, bool>()
    {
        { (float)CircleSwitchAngle.ZERO, true },
        { (float)CircleSwitchAngle.NINE, true },
        { (float)CircleSwitchAngle.ONEEIGHT, false },
        { (float)CircleSwitchAngle.TWOSEVEN, false }
    };

    Dictionary<float, CircleSwitchAngle> entryAngles = new Dictionary<float, CircleSwitchAngle>()
    {
        { 0, CircleSwitchAngle.ONEEIGHT },
        { 180, CircleSwitchAngle.ZERO },
        { 90, CircleSwitchAngle.TWOSEVEN },
        { 270, CircleSwitchAngle.NINE }
    };

    CircleSwitchAngle currentAngle;
    List<CircleSwitchAngle> switchOrder = new List<CircleSwitchAngle>()
    {
        CircleSwitchAngle.ZERO, CircleSwitchAngle.NINE, CircleSwitchAngle.ONEEIGHT, CircleSwitchAngle.TWOSEVEN
    };
    int currentAngleIndex = 0;
    bool locked = false;

    // Use this for initialization
    void Start()
    {
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
            CircleSwitchAngle entryAngle = entryAngles[ballAngle];
            var finalAngle = GetFinalAngle(entryAngle, currentAngle);

            // the ball didn't enter from the right direction
            if (!passageRules[finalAngle])
            {
                ball.Hit();
            }
        }
    }

    float GetFinalAngle(CircleSwitchAngle entryAngle, CircleSwitchAngle currentAngle)
    {
        float finalAngle = (float)entryAngle - (float)currentAngle;
        if (finalAngle < 0)
            finalAngle = 360 + finalAngle;

        return finalAngle;
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
