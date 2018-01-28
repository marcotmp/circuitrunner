using UnityEngine;

public class CameraFollows : MonoBehaviour {

    [HideInInspector]
    public GameObject target;
    public float z;

    public System.Action OnShakeComplete;
    private Vector3 currentPosition;
    private bool shakeMode = false;

    private void Start() { }

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

        Vector3 newpos = new Vector3(target.transform.position.x, target.transform.position.y, z);
        transform.position = newpos;
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
