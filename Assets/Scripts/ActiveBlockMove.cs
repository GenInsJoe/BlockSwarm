using UnityEngine;

public class ActiveBlockMove : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;
    

    // will always shoot towards the player
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        GameObject target = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = target.transform.position - this.transform.position;

        rb2d.AddForce(direction*speed);

	}

}
