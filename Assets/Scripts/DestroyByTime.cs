using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float BlockLifeTime;

    // Use this for initialization
    void Start () {
        // destroys the blocks after a certain amount of time
        Destroy(gameObject, BlockLifeTime);
        
	}

	
    
}
