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
        // stop
        speed = 0;
        // play collision animation
        gameObject.GetComponent<Animator>().Play("Bullet-Collision");

        Invoke("Disappear", .3f);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
