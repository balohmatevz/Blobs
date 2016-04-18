using UnityEngine;
using System.Collections;

public class IntroBox : MonoBehaviour
{

    public GameObject Ground;
    public bool Intro = true;
    float IntroNum = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Intro)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                IntroNum += Time.deltaTime;
                this.transform.Translate(0.1f * Time.deltaTime, 0, 0);
            }
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == Ground)
        {
            Debug.Log("hit");
            Intro = false;
            Player.p.gameObject.SetActive(true);
            Player.p.transform.position = new Vector3(this.transform.position.x, -3.68f, 0);
            this.gameObject.SetActive(false);
            Player.p.Audio_Fall.Play();
            Player.p.GameStart = true;
        }
    }
}
