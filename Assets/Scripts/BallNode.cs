using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNode : MonoBehaviour {
    public float speed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;

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
}
