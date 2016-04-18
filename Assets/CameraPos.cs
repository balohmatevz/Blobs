using UnityEngine;
using System.Collections;

public class CameraPos : MonoBehaviour { 

    Transform t;
    
	// Use this for initialization
	void Awake () {
	    t = this.transform;
	}

	// Update is called once per frame
	void Update () {
	    t.position = new Vector3(Player.p.transform.position.x, 0, -10);
	}
}
