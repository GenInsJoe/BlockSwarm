using UnityEngine;
using System.Collections;

public class DestroyBlockOnContact : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Block Destroyers")
        {
            print("Collision");
            Destroy(gameObject);
        }
    }
}
