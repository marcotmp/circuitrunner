using UnityEngine;

public class Bolt : MonoBehaviour {

    public float speed = 10f;
    public float lifespanSeconds = 2;

    void Start()
    {
        Invoke("Disappear", lifespanSeconds);
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void Hit()
    {
        Disappear();
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
