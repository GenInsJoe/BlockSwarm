using UnityEngine;
using System.Collections;

public class SpawnLocations : MonoBehaviour {

    public int id;
    private double x, y, z = 144;

	// Use this for initialization
	void Start () {
        x = Screen.width + 32;
        y = 83*(double)id;
	}

}
