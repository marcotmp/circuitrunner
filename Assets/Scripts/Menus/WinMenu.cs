using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour {

    public GameController gameController;
    public Button nextLevelBtn;
    public Button title;
    public Text thanksPlaying;

    public void ShowWinMenu()
    {
        gameObject.SetActive(true);
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= SceneManager.sceneCount)
        {
            thanksPlaying.gameObject.SetActive(true);
            nextLevelBtn.gameObject.SetActive(false);
        }
        else
        {
            thanksPlaying.gameObject.SetActive(false);
            nextLevelBtn.gameObject.SetActive(true);
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
