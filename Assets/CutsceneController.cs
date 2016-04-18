using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ANIMATION_GoToPlay() {
        SceneManager.LoadScene("Play");
    }
}
