using UnityEngine;

public class CameraFollows : MonoBehaviour {

    public GameObject target;
    public float z;
    public float lroffset = 5f;
    public float udoffset = 3f;
    public float smoothVelocity;

    private CameraTargetable targetable;

    private void Start()
    {
        targetable = target.GetComponent<CameraTargetable>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 targetPos = targetable.GetPosition();
        Vector3 newPos;

        switch (targetable.GetDirection())
        {
            case Direction.UP:
                newPos = new Vector3(targetPos.x, targetPos.y + udoffset, z);
                break;
            case Direction.RIGHT:
                newPos = new Vector3(targetPos.x + lroffset, targetPos.y, z);
                break;
            case Direction.DOWN:
                newPos = new Vector3(targetPos.x, targetPos.y - udoffset, z);
                break;
            case Direction.LEFT:
                newPos = new Vector3(targetPos.x - lroffset, targetPos.y, z);
                break;
            default:
                newPos = new Vector3(targetPos.x, targetPos.y, z);
                break;
        }

        transform.position = newPos;
    }
}
