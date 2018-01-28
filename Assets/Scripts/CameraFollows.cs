using UnityEngine;

public class CameraFollows : MonoBehaviour {

    public GameObject target;
    public float z;
    public float lroffset = 5f;
    public float udoffset = 3f;
    public float smoothVelocity;
    public System.Action OnShakeComplete;
    private CameraTargetable targetable;
    private Vector3 currentPosition;
    private bool shakeMode = false;

    private void Start()
    {
        targetable = target.GetComponent<CameraTargetable>();
    }

    // Update is called once per frame
    void Update () {

        if (shakeMode)
        {
            var x = Random.Range(-1, 1);
            var y = Random.Range(-1, 1);
            var z = Random.Range(-1, 1);
            transform.position = currentPosition + new Vector3(x, y, z) * 0.1f;

            return;
        }

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

    public void Shake()
    {
        shakeMode = true;
        currentPosition = transform.position;
        Invoke("StopShake", 0.3f);
    }

    public void StopShake()
    {
        shakeMode = false;
        if (OnShakeComplete != null)
            OnShakeComplete();
    }
}
