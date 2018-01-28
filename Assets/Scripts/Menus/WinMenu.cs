using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {

    public GameController gameController;
    
    public void OnPlayAgain()
    {
        gameController.RestartLevel();
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
