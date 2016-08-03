using UnityEngine;

public class BlockMove : MonoBehaviour {

    //Accelerations for the block
    public float speed;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(rb2d.mass * speed, 0));

	}

}
