// Author: Joe Bjorkman
// File: GameController.cs
// Last Updated: 8/15/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject pBlock1;
    public GameObject sBlock;
    public GameObject aBlock;
    public GameObject[] spawnpoints;
    public float startBuffer;
    public float[] tBetween = { 2F, 4F, 6F };
    public float minBetween;
    public double xSpawnBuf;
    private GameObject gmOvr;
    private List<int> passiveIndecies;
    private List<int> activeIndecies;
    private GameObject[] spawnPoints;
    private GameObject pBlock;
    private GameObject[] startButs;
    private float activeBuffer = 0.5F; // the time between the spawning of the passive blocks and the active blocks
    private int maxPassiveBlocks = 4;
    private int score; // increases by one for every wave of blocks (including empty ones) that the player passes
    private bool reverse; // reverses the order of the spawnpoints so that there is a little more fairness in the pick
    private bool shields;
    private int maxShields;
    
    public UnityEngine.UI.Text scoreboard;

    // Score Accessor
    public int Score
    {
        get { return score; }
    }

    // maxShields accessor and decrimentor
    public int MaxShields
    {
        get { return maxShields; }
    }

    public void DecrementMShields()
    {
        maxShields--;
    }

    public bool getShieldStatus
    {
        get { return shields; }
    }

	// Game initialization
	void Start ()
    {
        #region Initialization
        // initialize score and shields
        score = 0;        
        maxShields = 0;
        shields = false;
        UpdateScore();
        
        // initialize random flipper
        reverse = false;

        // initialize spawn location
        //setSpawnPoints();
        #endregion

        // find the game over game object
        gmOvr = GameObject.FindGameObjectWithTag("Game over");

        // hide the game over screen
        gmOvr.SetActive(false);
        // initialize used index lists
        passiveIndecies = new List<int>();
        activeIndecies = new List<int>();
        // get spawnpoints
        spawnPoints = GameObject.FindGameObjectsWithTag("Block spawners");

        print(spawnPoints.Length);

        // start the game paused
        Time.timeScale = 0;

        // start the game loop
        StartCoroutine(SpawnBlocks());

	}

    private void setSpawnPoints()
    {
        // all spawnpoints have the same x
        double x = Screen.width + xSpawnBuf;
        // Y value changes based on screen height. On default resolution of 980x640 should give top y location of 250
        double y = (Screen.height/2) - (.21875 * (Screen.height/2));
        // secondary y value (second from the top and bottom) based on 1/2 the previous y vaule
        
        // set the locations for the spawnpoints
        Vector3 [] spawnLocs = {new Vector3((float)x, (float)y, 0), new Vector3((float)x, (float)y/2, 0), 
                                new Vector3((float)x, 0, 0), new Vector3((float)x, (float)y/2*(-1), 0),
                                new Vector3((float)x, (float)(-y), 0)};
        for(int i = 0; i < 5; i++)
        {
            //spawnpoints[i] = spawnLocs[i];
        }


    }
	
    // update loop to check for loss
    void Update()
    {
        // if player has been destroyed
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            // stop the blocks from spawning, and the score from increasing
            StopAllCoroutines();

            // stop all movement on screen
            Time.timeScale = 0;

            // show the game over scene
            gmOvr.SetActive(true);
        }
    }

    public void SlowTime()
    {
        Time.timeScale = .5f;
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
            int minPassive = 3;
            if (score < 7)
            {
                tempMaxP = maxPassiveBlocks / 2;
                minPassive = 1;
            }
            else if (score < 15)
            {
                tempMaxP = maxPassiveBlocks / 4 * 3;
                minPassive = 1;
            }

            // Set the minimum number of blocks to spawn, this starts at 1 and increases to 2 and
            //   then 3 as the score increases
            if(score > 15 && score < 30)
            {
                minPassive = 2;
            }
            
            int numP2Spawn = Random.Range(minPassive, tempMaxP);

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
                // spawn the new pBlock
                Instantiate(pBlock1, getPassiveSpawnLocation(), Quaternion.identity);
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
            // wait a semi-random amount of time between each set of blocks this time will decrease
            //  as the score increases, to a minimum of minBetween
            float[] tempTimeBetween = tBetween;
            for(int i = 0; i < tempTimeBetween.Length -1; i++)
            {
                tempTimeBetween[i] = tempTimeBetween[i] - (Mathf.Round(score/10)/10);
                if(tempTimeBetween[i] < minBetween)
                {
                    tempTimeBetween[i] = minBetween;
                }
            }
            yield return new WaitForSeconds(tBetween[Random.Range(0, tempTimeBetween.Length - 1)]);

            reverse = !reverse;
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
            while (passiveIndecies.Contains((spawnLocIndex = Random.Range(0, spawnPoints.Length-1))))
            {
                continue;
            }
        }else
        {
            spawnLocIndex = Random.Range(0, spawnPoints.Length-1);
        }
        if(reverse){
            spawnLocIndex = spawnPoints.Length - 1 - spawnLocIndex;
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
        if(score%25 == 0)
        {
            maxShields++;
        }

        scoreboard.text = "Score: " + score;
    }

    public void allowShields()
    {
        shields = true;
    }

    public void disallowShields()
    {
        shields = false;
    }
}
