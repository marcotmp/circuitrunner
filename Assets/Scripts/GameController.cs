using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public BallNode ballNode;
    public CameraFollows cameraFollows;
    public GameObject gameOverMenu;

	// Use this for initialization
	void Start () {
        ballNode.OnDie = GameOver;
        ballNode.OnWin = Win;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update () {
	}

    public void GameOver()
    {
        // show gameover screen
        // camera shake
        cameraFollows.Shake();
        cameraFollows.OnShakeComplete = delegate ()
        {
            gameOverMenu.SetActive(true);
        };
    }

    public void Win()
    {
        print("win");
    }
}
