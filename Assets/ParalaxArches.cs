using UnityEngine;
using System.Collections;

public class ParalaxArches : MonoBehaviour
{

    Transform t;
    public float posZ = 10;
    public float posY = 0;
    float scale = 1;
    public float speedFactor = 2;

    // Use this for initialization
    void Awake()
    {
        t = this.transform;
        scale = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedFactor != 0)
        {
            t.position = new Vector3(Player.p.transform.position.x / speedFactor, posY, posZ);
        }
        t.Translate(Mathf.Round((Player.p.transform.position.x - t.position.x) / ((42.5f * scale) / 4)) * (42.5f * scale) / 4, 0, 0);
    }
}