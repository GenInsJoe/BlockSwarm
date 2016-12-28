using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnShield : MonoBehaviour {

    public UnityEngine.UI.Text ShieldCount;
    public GameObject shield;
    public GameController cntrl;
    public float shieldTime;
    private int numShields;
	// Use this for initialization
	void Start () {
        
        numShields = 1;
        UpdateShields();
        StartCoroutine(DeactivateShields());
	}
	
    public void SpawnNewShield()
    {
        if(numShields > 0)
        {
            print("Starting up the shield!");
            shield.SetActive(true);
            numShields--;
            UpdateShields();
            cntrl.DecrementMShields();
        }
    }

    void UpdateShields()
    {
        ShieldCount.text = "Shields Left: " + numShields;
    }

    void Update()
    {
        if (cntrl.getShieldStatus && Input.GetMouseButtonDown(0))
        {
            SpawnNewShield();
        }

        if (numShields < cntrl.MaxShields)
        {
            numShields++;
            UpdateShields();
        }
    }


    IEnumerator DeactivateShields()
    {
        while (true)
        {
            if (shield.activeSelf)
            {
                yield return new WaitForSeconds(shieldTime);
                shield.SetActive(false);
            }
        }
    }
}
