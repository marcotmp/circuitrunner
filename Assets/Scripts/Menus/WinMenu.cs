using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour {

    public GameController gameController;
    public Button nextLevelBtn;
    public Button title;

    public void ShowWinMenu()
    {
        gameObject.SetActive(true);
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCount)
        {
            nextLevelBtn.gameObject.SetActive(false);
            title.gameObject.SetActive(true);
        }

    }
    
    public void OnPlayAgain()
    {
        gameController.RestartLevel();
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OnTitleScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
