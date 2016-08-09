using UnityEngine;
using System.Collections;

public class CollideWithPassiveAndWalls : MonoBehaviour {

    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundry")
        {
            print("collided with wall");
            //rb2d.;
        }
    }
}
