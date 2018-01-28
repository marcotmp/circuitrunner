using UnityEngine;

public class StartPoint : MonoBehaviour {

    public enum EntranceDirection { RIGHT, LEFT, UP, DOWN };
    public EntranceDirection entranceDirection;

    public GameObject entranceRight;
    public GameObject entranceLeft;
    public GameObject entranceDown;
    public GameObject entranceUp;

    [HideInInspector]
    public GameObject ballNode;

    GameObject selectedEntrance;
    BallStartAngle ballStartAngle;
    
    public void StartGame()
    {
        ballNode.transform.position = transform.position;

        PlayEntrance();
    }

    void PlayEntrance()
    {
        switch(entranceDirection)
        {
            case EntranceDirection.RIGHT:
                selectedEntrance = entranceRight;
                ballStartAngle = BallStartAngle.RIGHT;
                break;
            case EntranceDirection.LEFT:
                selectedEntrance = entranceLeft;
                ballStartAngle = BallStartAngle.LEFT;
                break;
            case EntranceDirection.DOWN:
                selectedEntrance = entranceDown;
                ballStartAngle = BallStartAngle.DOWN;
                break;
            case EntranceDirection.UP:
                selectedEntrance = entranceUp;
                ballStartAngle = BallStartAngle.UP;
                break;
            default:
                selectedEntrance = entranceRight;
                ballStartAngle = BallStartAngle.RIGHT;
                break;
        }

        selectedEntrance.SetActive(true);
        selectedEntrance.GetComponent<Animator>().Play("Entrance");

        Invoke("InitBall", .1f);
    }

    void InitBall()
    {
        selectedEntrance.SetActive(false);

        FindObjectOfType<MusicManager>().PlayIntroEffect();
        ballNode.SetActive(true);
        ballNode.GetComponent<BallNode>().SetStartAngle(ballStartAngle);
        ballNode.GetComponent<BallNode>().StartRun();
    }
}
