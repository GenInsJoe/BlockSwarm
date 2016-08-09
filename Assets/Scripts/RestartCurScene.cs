using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartCurScene : MonoBehaviour {

	public void Restart() {
        print("in restart function");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
