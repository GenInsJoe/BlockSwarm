using UnityEngine;
using System.Collections;

public class DestroyBlockOnContact : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Block Destroyers")
        {
            print("Collision");
            Destroy(gameObject);
        }
    }
}
