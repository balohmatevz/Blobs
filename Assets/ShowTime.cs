using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTime : MonoBehaviour {

    Text t;

	// Use this for initialization
	void Start () {
	    t = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Player.p.GameStart) {
            t.text = "TIME: "+Mathf.FloorToInt(Player.p.T);
            if(Player.p.GameOver) {
                t.text = "TIME: "+(Mathf.FloorToInt(Player.p.T * 1000f) / 1000f);
            }
        }
        else
        {
            t.text = "TIME: PAUSED";
        }
	}
}
