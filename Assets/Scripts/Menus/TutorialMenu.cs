using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour {
    
    public void OnPlay()
    {
        // should exist because it doesn't destroy on load
        FindObjectOfType<MusicManager>().PlayGameplayClip();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
