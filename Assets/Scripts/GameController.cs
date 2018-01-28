using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public BallNode ballNode;
    public CameraFollows cameraFollows;
    public GameObject gameOverMenu;

    public StartPoint startPoint;

	void Start () {
        ballNode.OnDie = GameOver;
        ballNode.OnWin = Win;

        Invoke("StartGame", .5f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update () {
        startPoint.ballNode = ballNode.gameObject;
	}

    void StartGame()
    {
        startPoint.StartGame();
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
