using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    
    public GameObject ballNodePrefab;
    public CameraFollows cameraFollows;
    public GameObject gameOverMenu;
    public GameObject winMenu;

    public StartPoint startPoint;

    BallNode ballNode;

    private void Awake()
    {
        ballNode = Instantiate(ballNodePrefab).GetComponent<BallNode>();
        ballNode.gameObject.SetActive(false);

        cameraFollows.target = ballNode.gameObject;
        startPoint.ballNode = ballNode.gameObject;
    }

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
    void Update () { }

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
        winMenu.SetActive(true);
    }
}
