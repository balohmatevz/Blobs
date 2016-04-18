using UnityEngine;
using System.Collections;

public class Mouth : MonoBehaviour
{

    public Sprite Mouth1;
    public Sprite Mouth2;
    public Sprite Mouth3;
    public Sprite Mouth4;
    public SpriteRenderer sr;
    public float TransitionTimer = 0.4f;

    // Use this for initialization
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        TransitionTimer += Random.Range(0.1f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        TransitionTimer -= Time.deltaTime;
        if (TransitionTimer < 0)
        {
            TransitionTimer = 0.2f;
            if (Random.Range(0, 4) == 0)
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        sr.sprite = Mouth1;
                        break;
                    case 1:
                        sr.sprite = Mouth2;
                        break;
                    case 2:
                        sr.sprite = Mouth3;
                        break;
                    case 3:
                        sr.sprite = Mouth4;
                        break;
                }
            }
            else
            {
                sr.sprite = Mouth2;
            }
        }
    }
}
