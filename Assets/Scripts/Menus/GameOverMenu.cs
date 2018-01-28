using UnityEngine;

public class GameOverMenu : MonoBehaviour {

    public GameController gameController;

    public void OnPlay()
    {
        gameController.RestartLevel();
    }
}
