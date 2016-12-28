using UnityEngine;
using System.Collections;

public class RestrictMove : MonoBehaviour {

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boundry")
        {
            print("exiting screen. STOP RIGHT THERE!");
            Vector2 curV = rb2d.velocity;
            rb2d.transform.Translate(curV.x, -curV.y, 0.0f);
        }
    }
}
