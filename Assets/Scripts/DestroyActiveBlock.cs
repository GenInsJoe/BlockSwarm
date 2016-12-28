using UnityEngine;
using System.Collections;

public class DestroyActiveBlock : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2s(Collider2D other) {
        if(other.tag == "ActiveBlock")
        {
            Destroy(other);
        }
	
	}
}
