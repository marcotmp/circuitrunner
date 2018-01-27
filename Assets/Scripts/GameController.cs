using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public BallNode ballNode;
    public CameraFollows cameraFollows;

	// Use this for initialization
	void Start () {
        ballNode.OnDie = GameOver;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GameOver()
    {
        // show gameover screen
        // camera shake
        cameraFollows.Shake();
    }
}
