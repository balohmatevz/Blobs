using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public static Player p;

    public float T;
    const float MAX_SPEED = 9f;
    const float SPEEDUP_FACTOR = 64f;
    const float SLOWDOWN_FACTOR = 256f;
    const float JUMP_DELAY = 0.5f;

    public int LevelID = 0;

    public string PlayerName = "Unassigned";
    public AudioSource Audio_Jump;
    public AudioSource Audio_Step;
    public AudioSource Audio_Fall;
    public AudioSource Audio_Transform;
    public AudioSource Audio_Music;

    public CanvasGroup GameOverFader;
    public float GameOverTimer = 0;

    public GameObject GroundCheckPoint;
    public GameObject LeftCheckPoint;
    public GameObject RightCheckPoint;
    public GameObject IntroBox;
    public Animator animator;
    public GameObject PlayerVisual;
    public SpriteRenderer PlayerBody;
    public SpriteRenderer PlayerShoe1;
    public SpriteRenderer PlayerShoe2;

    public Text scoreboard;

    public bool GameOver = false;
    public bool GameStart = false;
    bool Intro = true;
    float IntroNum = 0;
    float JumpDelay = 0;
    float ShapeShiftDelay = 0;
    bool OnGround = false;
    bool LeftHit = false;
    bool RightHit = false;
    float InnertiaX = 0;
    Rigidbody2D rb;
    int ShapeNum = 0;
    public float StepTimer = 0.4f;

    // Use this for initialization
    void Awake()
    {
        p = this;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
        GameOverFader.gameObject.SetActive(false);
        LevelID = Settings.mapid;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }

        if (GameOver)
        {
            GameOverFader.gameObject.SetActive(true);
            GameOverTimer += Time.deltaTime;
            GameOverFader.alpha = GameOverTimer - 2f;
        }

        StepTimer -= Time.deltaTime;
        if (StepTimer < -10)
        {
            StepTimer = -10;
        }
        JumpDelay -= Time.deltaTime;
        if (JumpDelay < -10)
        {
            JumpDelay = -10;
        }
        ShapeShiftDelay -= Time.deltaTime;
        if (ShapeShiftDelay < -10)
        {
            ShapeShiftDelay = -10;
        }

        if (!GameOver)
        {
            T += Time.deltaTime;
        }
        InnertiaX = Input.GetAxis("Horizontal");

        RaycastHit2D groundHit = Physics2D.Raycast(GroundCheckPoint.transform.position, Vector2.right, 1);
        Debug.DrawRay(GroundCheckPoint.transform.position, Vector2.right, Color.green);
        if (groundHit.transform)
        {
            OnGround = true;
            if (groundHit.transform.gameObject.name == "Level End" || groundHit.transform.gameObject.name == "Level End(Clone)")
            {
                SetGameOver();
            }
        }
        else
        {
            OnGround = false;
        }

        RaycastHit2D leftHit = Physics2D.Raycast(LeftCheckPoint.transform.position, Vector2.down, 1.15f);
        Debug.DrawRay(LeftCheckPoint.transform.position, Vector2.down, Color.green);
        if (leftHit.transform && leftHit.transform.gameObject.layer != 9 && leftHit.transform.gameObject.layer != 10)
        {
            LeftHit = true;
            if (leftHit.transform.gameObject.name == "Level End" || leftHit.transform.gameObject.name == "Level End(Clone)")
            {
                SetGameOver();
            }
        }
        else
        {
            LeftHit = false;
        }

        RaycastHit2D rightHit = Physics2D.Raycast(RightCheckPoint.transform.position, Vector2.down, 1.15f);
        Debug.DrawRay(RightCheckPoint.transform.position, Vector2.down, Color.green);
        if (rightHit.transform && rightHit.transform.gameObject.layer != 9 && rightHit.transform.gameObject.layer != 10)
        {
            RightHit = true;
            if (rightHit.transform.gameObject.name == "Level End" || rightHit.transform.gameObject.name == "Level End(Clone)")
            {
                SetGameOver();
            }
        }
        else
        {
            RightHit = false;
        }

        if (OnGround && JumpDelay < 0 && (Input.GetAxis("Jump") > 0 || Input.GetKey("w"))) //RELEASE - remove Input.GetKey("w")
        {
            rb.AddForce(Vector2.up * 700f);
            JumpDelay = JUMP_DELAY;
            Audio_Jump.Play();
        }

        if (ShapeShiftDelay < 0 && (Input.GetAxis("Fire1") > 0))
        {
            ShapeShift();
            ShapeShiftDelay = JUMP_DELAY;
            Audio_Transform.Play();
        }

        // Debug.DrawRay(GroundCheckPoint.transform.position, Vector2.right, Color.red);

        rb.velocity = new Vector2(InnertiaX * 400 * Time.deltaTime, rb.velocity.y);

        if (rb.velocity.x < 0)
        {
            PlayerVisual.transform.localScale = new Vector3(-1, 1, 1);
            if (LeftHit)
            {
                rb.velocity = new Vector3(0, rb.velocity.y);
            }
        }
        if (rb.velocity.x > 0)
        {
            PlayerVisual.transform.localScale = new Vector3(1, 1, 1);
            if (RightHit)
            {
                rb.velocity = new Vector3(0, rb.velocity.y);
            }
        }

        if (rb.velocity.x > 10)
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
        }
        else if (rb.velocity.x < -10)
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            animator.SetBool("Walking", true);
            if (StepTimer < 0 && OnGround)
            {
                Audio_Step.Play();
                StepTimer = 0.3f;
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Level End" || coll.gameObject.name == "Level End(Clone)")
        {
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        if (!GameOver)
        {
            this.GameOver = true;
            if (LevelID == 0)
            {
                Settings.PlayedTutorial = true;
                Settings.SaveSettings();
            }
            WWW www2 = new WWW("http://liamlime.com/api/1.0/ld35/submitscore/?levelID=" + WWW.EscapeURL(LevelID.ToString()) + "&time=" + WWW.EscapeURL(T.ToString()) + "&name=" + WWW.EscapeURL(PlayerName));

            string url = "http://liamlime.com/api/1.0/ld35/submitscore/index.php?levelID=" + WWW.EscapeURL(LevelID.ToString());
            WWW www = new WWW(url);

            //float timeout = 1000;
            while (!www.isDone)
            {

            }

            Debug.Log(url);
            scoreboard.text = www.text;
        }
    }

    public void ShapeShift()
    {
        switch (ShapeNum)
        {
            case 0:
                //Is red, change to blue
                PlayerBody.color = new Color((float)0x61 / 0xFF, (float)0x61 / 0xff, (float)0xFF / 0xFF);
                PlayerShoe1.color = new Color((float)0x00 / 0xFF, (float)0x00 / 0xFF, (float)0x9B / 0xFF);
                PlayerShoe2.color = new Color((float)0x00 / 0xFF, (float)0x00 / 0xFF, (float)0xC8 / 0xFF);
                this.gameObject.layer = 12;
                ShapeNum = 1;
                break;
            case 1:
                //Is blue, change to red
                PlayerBody.color = new Color((float)0xFF / 0xFF, (float)0x61 / 0xFF, (float)0x61 / 0xff);
                PlayerShoe1.color = new Color((float)0x9B / 0xFF, (float)0x00 / 0xFF, (float)0x00 / 0xFF);
                PlayerShoe2.color = new Color((float)0xC8 / 0xFF, (float)0x00 / 0xFF, (float)0x00 / 0xFF);
                this.gameObject.layer = 11;
                ShapeNum = 0;
                break;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}

