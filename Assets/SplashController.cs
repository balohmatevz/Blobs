using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {
    
    public AudioSource Audio_LiamLime;
    public AudioSource Audio_LudumDare;

	// Use this for initialization
	void Start () {
        Settings.LoadSettings();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ANIMATION_PlayAudioLL() {
        Audio_LiamLime.Play();
    }

    public void ANIMATION_PlayAudioLD() {
        Audio_LudumDare.Play();
    }

    public void ANIMATION_SceneTransition() {
        SceneManager.LoadScene("menu");
    }
}
