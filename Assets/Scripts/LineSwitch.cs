using System.Collections.Generic;
using UnityEngine;

public class LineSwitch : MonoBehaviour {

    public enum LineSwitchAngles { ZERO=0, NINE=90, ONEEIGHT=180, TWOSEVEN=270 };
    public LineSwitchAngles startingAngle;
    
    // key - switch angle
    // value - ball angle
    Dictionary<float, float> matchingAngles = new Dictionary<float, float>()
    {
        { (float)LineSwitchAngles.ZERO, 0 },
        { (float)LineSwitchAngles.NINE, 90 },
        { (float)LineSwitchAngles.ONEEIGHT, 0 },
        { (float)LineSwitchAngles.TWOSEVEN, 90 }
    };

    LineSwitchAngles currentAngle;
    List<LineSwitchAngles> switchOrder = new List<LineSwitchAngles>()
    {
        LineSwitchAngles.ZERO, LineSwitchAngles.NINE, LineSwitchAngles.ONEEIGHT, LineSwitchAngles.TWOSEVEN
    };
    int currentAngleIndex = 0;
    bool locked = false;

    // Use this for initialization
    void Start () {
        currentAngle = startingAngle;
        currentAngleIndex = switchOrder.IndexOf(currentAngle);
        UpdateAngle();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bolt projectile = collision.GetComponent<Bolt>();
        if (projectile != null)
        {
            if (!locked)
            {
                // signal projectile to destroy itself
                projectile.OnHit();

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
            float angle = (float)ball.GetDirection();
            float acceptedAngle = matchingAngles[(float)currentAngle];
            if (acceptedAngle != angle)
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
