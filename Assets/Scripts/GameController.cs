// Author: Joe Bjorkman
// File: GameController.cs
// Last Updated: 7/29/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject pBlock;
    public GameObject aBlock;
    public float startBuffer;
    public float[] tBetween = { 2F, 4F, 6F };

    private List<int> passiveIndecies;
    private List<int> activeIndecies;
    private GameObject[] spawnPoints;
    private float activeBuffer = 0.5F; // the time between the spawning of the passive blocks and the active blocks
    private int maxPassiveBlocks = 4;
    private int score; // increases by one for every wave of blocks (including empty ones) that the player passes

    public UnityEngine.UI.Text scoreboard;

	// Game initialization
	void Start () {
        // initialize score
        score = 0;
        UpdateScore();

        // initialize used index lists
        passiveIndecies = new List<int>();
        activeIndecies = new List<int>();
        
        // get spawnpoints
        spawnPoints = GameObject.FindGameObjectsWithTag("Block spawners");

        // start the game loop
        StartCoroutine(SpawnBlocks());

	}
	
    // update loop to check for loss
    void Update()
    {
        // if player has been destroyed
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            // stop the blocks from spawning, and the score from increasing
            StopAllCoroutines();
        }
    }

    // Coroutine for for spawning obstacle blocks
    IEnumerator SpawnBlocks()
    {
        yield return new WaitForSeconds(startBuffer);
        // start spawn loop
        while (true)
        {
            // Get the number of passive blocks to spawn. This will increase as the score goes up
            int tempMaxP = maxPassiveBlocks;
            if (score < 7)
            {
                tempMaxP = maxPassiveBlocks / 2;
            }
            else if (score < 15)
            {
                tempMaxP = maxPassiveBlocks / 4 * 3;
            }
            int numP2Spawn = Random.Range(1, tempMaxP);

            // Determine whether any active blocks will spawn and how many will spawn. 
            //  There is a 20% chance that an active block will spawn while score < 10, 
            //  a 40% chance if 10 <= Score < 50 and a 60% if score >= 50. Will spawn
            //  2 if there is a "critical" on the random number (if the random value is
            //  a 1). As of 7/29/2016 there is a maximum of 2 active blocks
            int numA2Spawn = 0;
            int activeSpawnRoll = Random.Range(1, 10);
            if (activeSpawnRoll == 1)
            {
                numA2Spawn = 2;
            }
            else if ((score < 10 && activeSpawnRoll <= 2) || (score < 50 && activeSpawnRoll <= 4) || (score >= 50 && activeSpawnRoll < 5))
            {
                numA2Spawn = 1;
            }

            // spawn passive blocks first
            for(int i = 0; i < numP2Spawn; i++)
            {
                
                Instantiate(pBlock, getPassiveSpawnLocation(), Quaternion.identity);
            }

            // spawn active Blocks [activeBuffer]s after last spawn (so if two spawn the second
            //  one would spawn 2*[activeBuffer]s later and in a different position)
            for(int i =0; i < numA2Spawn; i++)
            {
                // wait before spawning
                yield return new WaitForSeconds(activeBuffer);
                Instantiate(aBlock, getActiveSpawnLocation(), Quaternion.identity);
            }
            // reset the used indecies
            passiveIndecies.Clear();
            activeIndecies.Clear();
            // wait a semi-random amount of time between each set of blocks
            yield return new WaitForSeconds(tBetween[Random.Range(0, tBetween.Length - 1)]);

            // update the score
            incrementScore();
        }
    }

    // gets a spawn location 
    private Vector3 getPassiveSpawnLocation()
    {
        // make sure two blocks don't spawn in the same space
        
        int spawnLocIndex;
        if (passiveIndecies != null)
        {
            while (passiveIndecies.Contains((spawnLocIndex = Random.Range(0, spawnPoints.Length - 1))))
            {
                continue;
            }
        }else
        {
            spawnLocIndex = Random.Range(0, spawnPoints.Length - 1);
        }

        passiveIndecies.Add(spawnLocIndex);
        // get the location of the spawnpoint
        Vector3 ret = new Vector3(spawnPoints[spawnLocIndex].transform.position.x, spawnPoints[spawnLocIndex].transform.position.y);
        return ret;
    }

    private Vector3 getActiveSpawnLocation()
    {
        // make sure two blocks don't spawn in the same space

        int spawnLocIndex;
        if (activeIndecies != null)
        {
            while (activeIndecies.Contains((spawnLocIndex = Random.Range(0, spawnPoints.Length - 1))))
            {
                continue;
            }
        }
        else
        {
            spawnLocIndex = Random.Range(0, spawnPoints.Length - 1);
        }

        activeIndecies.Add(spawnLocIndex);

        // get the location of the spawnpoint
        Vector3 ret = new Vector3(spawnPoints[spawnLocIndex].transform.position.x, spawnPoints[spawnLocIndex].transform.position.y);
        return ret;
    }

    // increases the score by 1
    public void incrementScore()
    {
        score++;
        UpdateScore();
    }

    // update what the scoreboard says
	void UpdateScore () {
        scoreboard.text = "Score: " + score;
    }   
}
