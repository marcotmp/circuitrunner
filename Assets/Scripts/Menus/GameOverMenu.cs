using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameController gameController;

    public void OnPlay()
    {
        gameController.RestartLevel();
    }

    public void OnTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
