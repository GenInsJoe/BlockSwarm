using UnityEngine;
using System.Collections;

public class PauseUnpause : MonoBehaviour {

    // continue/start the game
    public void Unpause()
    {
        Time.timeScale = 1;
    }

    // pause the game
    public void Pause()
    {
        Time.timeScale = 0;
    }
}
