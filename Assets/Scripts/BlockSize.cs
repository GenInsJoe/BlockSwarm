using UnityEngine;
using System.Collections;

public class BlockSize : MonoBehaviour {


    private double blkScale;
	// Use this for initialization
	void Start () {
        // Gives us a value of 1.8 with the default screen size of 980x640
        blkScale = 1152 / Screen.height;
	}
	
}
