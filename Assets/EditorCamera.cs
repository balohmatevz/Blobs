using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class EditorCamera : MonoBehaviour
{
    public GameObject TerrainPivot;
    public GameObject PF_BOX;
    public GameObject PF_CLAW;
    public GameObject PF_PLANK_RED;
    public GameObject PF_PLANK_BLUE;
    public GameObject PF_DOOR_RED;
    public GameObject PF_DOOR_BLUE;
    public GameObject PF_TEXT;
    public GameObject PF_TERRAIN;
    public GameObject PF_BACKGROUND;
    public GameObject PF_LEVEL_END;
    public Sprite SR_Handle;

    public RectTransform Hints;

    public InputField ifposx;
    public InputField ifposy;
    public InputField ifposz;
    public InputField ifrotx;
    public InputField ifroty;
    public InputField ifrotz;
    public InputField ifscalex;
    public InputField ifscaley;
    public InputField ifscalez;
    public Text idText;

    float MouseMoveThreshold = 0.1f;
    bool MouseMoveThresholdPassed = false;
    Vector2 MouseClickPosition;

    public GameObject SelectedObject = null;
    public Color SelectedObjectColor;
    public int LevelID = 0;

    // Use this for initialization
    void Start()
    {
        LevelID = Random.Range(1, 99999999);
        if (Settings.mapid == 0)
        {
            SpawnLevel("ID!0|BOX!-2.327!0.5030003!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-2.402!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-2.258!2.493!0!0!0!0!1!1!1!0.8897059!0.5364403!0.5364403!0.5126327!0.4879434!0.6985294|BOX!-2.226!3.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.557!1.502!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-2.625!4.486!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.45!2.493!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-4.641!2.681!0!0!0!307.0319!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.288!3.49!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-6.5!1.531!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-4.76!1.502!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-4.68!0.5030003!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-5.9!0.5030003!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-7.08!0.5210001!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-5.608!2.324!0!0!0!37.23653!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-6.93!2.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-7.17!3.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-7.58!1.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-8.15!0.5110002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-9.269!0.5040002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-10.357!0.5040002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-8.842!1.505!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-9.893!1.498!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-9.661!2.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-8.602!2.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-9.487!3.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-8.349!3.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-7.902!4.498!0!0!0!4.518214!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-10.64!3.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-10.82!2.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-11.05!1.498!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-11.51!0.5040002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-12.68!0.5040002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-12.22!1.498!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-11.99!2.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-11.81!3.506!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.476!0.5030003!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-4.372!3.675!0!0!0!343.5968!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.661!4.564!0!0!0!355.1928!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-6.055!3.268!0!0!0!28.85226!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-5.386!4.075!0!0!0!343.8689!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-4.607!4.914!0!0!0!350.0614!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!-3.497!5.552!0!0!0!354.3332!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!26.73!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!27.82!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!28.93!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!30.01!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!29.43!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!29.82!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!43.49!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!43.1!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!42.27!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!41.19!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!40.08!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!38.99!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!44.29!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!43.448!0.5150001!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!44.522!0.5150001!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!45.58!0.5070002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!48.51109!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!49.54!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!50.63!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!51.72!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!55.86!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!56.94!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!60.72!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!61.8!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!65.32!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|CLAW!69.17!4.601!-5!0!0!0!1!1!1!0.3823529!0.3823529!0.3823529!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!72.56!0.5070002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!73.71!0.5070002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!74.75!0.5070002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!75.94!0.5070002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!72.36!1.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!72.53!2.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!72.27!3.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!72.23!4.541!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!73.42!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!74.53!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!73.64!2.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!75.6!1.531!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!73.34!3.481!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!73.31!4.481!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!74.44!3.481!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!74.73!2.521!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!74.37!4.481!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!75.8!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!76.01!3.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!76.38!4.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!75.34!4.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!76.86!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!76.91!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!76.9!2.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!77.03!3.491!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!89.8!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!90.84!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!88.72!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!89.8!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!88.74001!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!88.73!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!102.34!3.493!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!90.84!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!101.4!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!102.48!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!101.32!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!101.3!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!101.27!3.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!102.38!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!102.37!2.492!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!103.36!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!104.45!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!103.45!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!115.49!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!114.45!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!113.37!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!114.45!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!113.39!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!113.38!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!127.2!2.492!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!126.1!3.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!127.31!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!126.23!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!128.19!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!127.21!1.501!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!129.28!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!127.17!3.493!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!128.28!0.5010002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!126.13!2.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!126.15!1.511!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|PLANK_RED!121.37!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!96.77!3.441!0!0!0!0!1!1!1!5.460342|DOOR_RED!137.84!1.891!0!0!0!0!1!1!1|DOOR_BLUE!141.58!1.891!0!0!0!0!1!1!1|DOOR_BLUE!149.23!1.891!0!0!0!0!1!1!1|DOOR_RED!145.49!1.891!0!0!0!0!1!1!1|DOOR_RED!153.1!1.891!0!0!0!0!1!1!1|DOOR_BLUE!156.84!1.891!0!0!0!0!1!1!1|BOX!164.16!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!165.21!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!166.28!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!167.42!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!168.51!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!169.76!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!170.9!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!172.17!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!173.22!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!172.76!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!171.49!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!170.35!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!169.1!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!168.01!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!166.87!1.495!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!168.6!2.497!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!169.85!2.497!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!170.99!2.497!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!172.26!2.497!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!185.97!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|BOX!197.89!0.4910002!0!0!0!0!1!1!1!0.625!0.625!0.625!0.8970588!0.8970588!0.8970588|PLANK_RED!179.11!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!190.76!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!202.67!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!217.61!1.81!0!0!0!0!1!1!1!5.460342|PLANK_RED!224.54!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!232.47!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!240.18!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!247.41!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!254.12!3.441!0!0!0!0!1!1!1!5.460342|PLANK_RED!260.76!3.441!0!0!0!0!1!1!1!5.460342|DOOR_RED!184.2!5.45!0!0!0!0!1!1!1|DOOR_BLUE!196.11!5.49!0!0!0!0!1!1!1|TERRAIN!184.19!1.793!-1!0!0!0!2.341964!3.605462!1!0.5569741!0.7058823!0.4982699|TERRAIN!196.12!1.793!-1!0!0!0!2.341964!3.605462!1!1!1!1|TERRAIN!207.79!1.793!-1!0!0!0!2.341964!3.605462!1!1!1!1|TERRAIN!266.61!1.793!-1!0!0!0!2.341964!3.605462!1!1!1!1|TERRAIN!313.67!6.201!-1!0!0!0!69.91353!9.493447!1!1!1!1|BACKGROUND!278.17!0.7810001!3!0!0!0!1.138261!1.578845!1!1!1!1|BACKGROUND!278.37!0.7210002!2.9!0!0!0!0.7453932!1.453262!1!0!0!0|TERRAIN!278.17!6.201!3!0!0!0!1.138261!9.500402!1!1!1!1|BACKGROUND!313.65!0.8410001!-1!0!0!0!69.98606!1.693015!1!1!1!1|LEVEL_END!279.43!0.54!0!0!0!0!1!1!1|TEXT!-0.044!5.618!0!0!0!0!0.1!0.1!1!A / D to move|TEXT!26.28!5.618!0!0!0!0!0.1!0.1!1!Space to jump|TEXT!114.35!6.881!0!0!0!0!0.1!0.1!1!E to shapeshift");
        }
        else
        {
            SpawnLevel(Settings.mapList[Settings.mapid]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
        idText.text = "MAP ID:\n"+LevelID;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.03f)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * 20f * Time.deltaTime, 0, 0);
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            MouseClickPosition = mousePosition;
            MouseMoveThresholdPassed = false;

            if (hitCollider != null && hitCollider.transform)
            {
                GameObject go = hitCollider.transform.gameObject;

                if (go.GetComponent<InputField>() != null)
                {
                    return;
                }

                int safety = 1000;
                while (safety > 0 && go.transform.parent.gameObject.name != "Terrain Pivot")
                {
                    go = go.transform.parent.gameObject;
                    safety--;
                }
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                if (sr != null || mr != null)
                {
                    if (SelectedObject != null)
                    {
                        SpriteRenderer sr2 = SelectedObject.GetComponent<SpriteRenderer>();
                        if (sr2 != null)
                        {
                            sr2.color = SelectedObjectColor;
                            SelectedObject = null;
                            ifposx.text = "";
                            ifposy.text = "";
                            ifposz.text = "";
                            ifrotx.text = "";
                            ifroty.text = "";
                            ifrotz.text = "";
                            ifscalex.text = "";
                            ifscaley.text = "";
                            ifscalez.text = "";
                        }
                    }

                    if (sr != null)
                    {
                        SelectedObjectColor = sr.color;
                        sr.color = Color.green;
                    }
                    SelectedObject = go;
                    ifposx.text = SelectedObject.transform.localPosition.x.ToString();
                    ifposy.text = SelectedObject.transform.localPosition.y.ToString();
                    ifposz.text = SelectedObject.transform.localPosition.z.ToString();
                    ifrotx.text = SelectedObject.transform.localRotation.x.ToString();
                    ifroty.text = SelectedObject.transform.localRotation.y.ToString();
                    ifrotz.text = SelectedObject.transform.localRotation.z.ToString();
                    ifscalex.text = SelectedObject.transform.localScale.x.ToString();
                    ifscaley.text = SelectedObject.transform.localScale.y.ToString();
                    ifscalez.text = SelectedObject.transform.localScale.z.ToString();

                }
            }



        }

        if (Input.GetMouseButtonDown(1))
        {
            if (SelectedObject != null)
            {
                SpriteRenderer sr = SelectedObject.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = SelectedObjectColor;
                    SelectedObject = null;
                    ifposx.text = "";
                    ifposy.text = "";
                    ifposz.text = "";
                    ifrotx.text = "";
                    ifroty.text = "";
                    ifrotz.text = "";
                    ifscalex.text = "";
                    ifscaley.text = "";
                    ifscalez.text = "";
                }
            }
        }

        if (Input.GetMouseButton(0))
        {

            float dist = Vector2.Distance(mousePosition, MouseClickPosition);
            if (dist > MouseMoveThreshold)
            {
                MouseMoveThresholdPassed = true;
            }
            if (MouseMoveThresholdPassed)
            {
                if (SelectedObject != null)
                {
                    SelectedObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, SelectedObject.transform.position.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseMoveThresholdPassed = false;
            if (SelectedObject != null)
            {
                ifposx.text = SelectedObject.transform.localPosition.x.ToString();
                ifposy.text = SelectedObject.transform.localPosition.y.ToString();
                ifposz.text = SelectedObject.transform.localPosition.z.ToString();
                ifrotx.text = SelectedObject.transform.localRotation.x.ToString();
                ifroty.text = SelectedObject.transform.localRotation.y.ToString();
                ifrotz.text = SelectedObject.transform.localRotation.z.ToString();
                ifscalex.text = SelectedObject.transform.localScale.x.ToString();
                ifscaley.text = SelectedObject.transform.localScale.y.ToString();
                ifscalez.text = SelectedObject.transform.localScale.z.ToString();
            }
        }

        if (Input.GetKeyDown("delete"))
        {
            if (SelectedObject != null)
            {
                Destroy(SelectedObject);
                SelectedObject = null;
                ifposx.text = "";
                ifposy.text = "";
                ifposz.text = "";
                ifrotx.text = "";
                ifroty.text = "";
                ifrotz.text = "";
                ifscalex.text = "";
                ifscaley.text = "";
                ifscalez.text = "";
            }
        }

        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.01f)
        {
            if (SelectedObject != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Vector3 vs = SelectedObject.transform.localScale;
                    vs.x += Input.GetAxis("Mouse ScrollWheel") * 1f;
                    vs.x = Mathf.Clamp(vs.x, 0.01f, 100);
                    SelectedObject.transform.localScale = vs;
                }
                else
                {
                    Vector3 vs = SelectedObject.transform.localScale;
                    vs.y += Input.GetAxis("Mouse ScrollWheel") * 1f;
                    vs.y = Mathf.Clamp(vs.y, 0.01f, 100);
                    SelectedObject.transform.localScale = vs;
                }
            }
        }
    }

    public void SpawnObject(string obj)
    {
        SpawnObjectGO(obj);
    }

    public GameObject SpawnObjectGO(string obj)
    {
        GameObject go = null;
        switch (obj)
        {
            case "BOX":
                go = Instantiate(PF_BOX);
                break;
            case "CLAW":
                go = Instantiate(PF_CLAW);
                break;
            case "PLANK RED":
                go = Instantiate(PF_PLANK_RED);
                break;
            case "PLANK BLUE":
                go = Instantiate(PF_PLANK_BLUE);
                break;
            case "DOOR RED":
                go = Instantiate(PF_DOOR_RED);
                break;
            case "DOOR BLUE":
                go = Instantiate(PF_DOOR_BLUE);
                break;
            case "TEXT":
                go = Instantiate(PF_TEXT);
                break;
            case "WALL":
                go = Instantiate(PF_TERRAIN);
                break;
            case "BG":
                go = Instantiate(PF_BACKGROUND);
                break;
            case "END":
                go = Instantiate(PF_LEVEL_END);
                break;
        }

        if (go != null)
        {
            go.transform.SetParent(TerrainPivot.transform);
            go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
            if (bc == null)
            {
                go.AddComponent<BoxCollider2D>();
            }
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                GameObject gosr = new GameObject();
                gosr.transform.SetParent(go.transform);
                gosr.transform.localPosition = Vector3.zero;
                gosr.transform.localRotation = Quaternion.identity;
                gosr.transform.localScale = Vector3.one;
                sr = go.AddComponent<SpriteRenderer>();
                gosr.AddComponent<SpriteRenderer>();
                sr = gosr.GetComponent<SpriteRenderer>();
                sr.sprite = SR_Handle;
            }
        }

        return go;
    }

    public void ApplyParamsToSelectedObject()
    {
        if (SelectedObject != null)
        {
            float posx = 0;
            float posy = 0;
            float posz = 0;
            float rotx = 0;
            float roty = 0;
            float rotz = 0;
            float scalex = 0;
            float scaley = 0;
            float scalez = 0;

            if (!float.TryParse(ifposx.text, out posx))
            {
                posx = 0;
            }
            if (!float.TryParse(ifposy.text, out posy))
            {
                posy = 0;
            }
            if (!float.TryParse(ifposz.text, out posz))
            {
                posz = 0;
            }
            if (!float.TryParse(ifrotx.text, out rotx))
            {
                rotx = 0;
            }
            if (!float.TryParse(ifroty.text, out roty))
            {
                roty = 0;
            }
            if (!float.TryParse(ifrotz.text, out rotz))
            {
                rotz = 0;
            }
            if (!float.TryParse(ifscalex.text, out scalex))
            {
                scalex = 0;
            }
            if (!float.TryParse(ifscaley.text, out scaley))
            {
                scaley = 0;
            }
            if (!float.TryParse(ifscalez.text, out scalez))
            {
                scalez = 0;
            }


            SelectedObject.transform.localPosition = new Vector3(posx, posy, posz);
            SelectedObject.transform.localRotation = Quaternion.Euler(new Vector3(rotx, roty, rotz));
            SelectedObject.transform.localScale = new Vector3(scalex, scaley, scalez);
        }
    }

    public void ShowHideHints()
    {
        Hints.gameObject.SetActive(!Hints.gameObject.activeSelf);
    }


    public void CreateSave()
    {
        string SaveString = "ID!" + LevelID;
        foreach (Transform tr in TerrainPivot.transform)
        {
            GameObject go = tr.gameObject;
            string SavePart = "";
            Vector3 p = go.transform.localPosition;
            Vector3 r = go.transform.localRotation.eulerAngles;
            Vector3 s = go.transform.localScale;
            switch (go.name)
            {
                case "Box":
                case "Box(Clone)":
                    GameObject go_box_light = go.transform.FindChild("Box Light").gameObject;
                    SpriteRenderer sr_outer = go.GetComponent<SpriteRenderer>();
                    SpriteRenderer sr_inner = go_box_light.GetComponent<SpriteRenderer>();
                    SavePart = "BOX!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + sr_outer.color.r + "!" + sr_outer.color.g + "!" + sr_outer.color.b + "!" + sr_inner.color.r + "!" + sr_inner.color.g + "!" + sr_inner.color.b;
                    break;
                case "Claw":
                case "Claw(Clone)":
                    GameObject go_claw_claw = go.transform.FindChild("Claw").gameObject;
                    GameObject go_claw_o = go.transform.FindChild("Box").gameObject;
                    GameObject go_claw_i = go_claw_o.transform.FindChild("Box Light").gameObject;
                    SpriteRenderer sr_claw = go_claw_claw.GetComponent<SpriteRenderer>();
                    SpriteRenderer sr_claw_o = go_claw_o.GetComponent<SpriteRenderer>();
                    SpriteRenderer sr_claw_i = go_claw_i.GetComponent<SpriteRenderer>();
                    SavePart = "CLAW!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + sr_claw.color.r + "!" + sr_claw.color.g + "!" + sr_claw.color.b + "!" + sr_claw_o.color.r + "!" + sr_claw_o.color.g + "!" + sr_claw_o.color.b + "!" + sr_claw_i.color.r + "!" + sr_claw_i.color.g + "!" + sr_claw_i.color.b;
                    break;
                case "Plank Red":
                case "Plank Red(Clone)":
                    GameObject plank1 = go.transform.FindChild("Plank").gameObject;
                    SavePart = "PLANK_RED!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + plank1.transform.localScale.x;
                    break;
                case "Plank Blue":
                case "Plank Blue(Clone)":
                    GameObject plank2 = go.transform.FindChild("Plank").gameObject;
                    SavePart = "PLANK_BLUE!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + plank2.transform.localScale.x;
                    break;
                case "Door Red":
                case "Door Red(Clone)":
                    SavePart = "DOOR_RED!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z;
                    break;
                case "Door Blue":
                case "Door Blue(Clone)":
                    SavePart = "DOOR_BLUE!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z;
                    break;
                case "Terrain":
                case "Terrain(Clone)":
                    SpriteRenderer sr_ter = go.GetComponent<SpriteRenderer>();
                    SavePart = "TERRAIN!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + sr_ter.color.r + "!" + sr_ter.color.g + "!" + sr_ter.color.b;
                    break;
                case "Background":
                case "Background(Clone)":
                    SpriteRenderer sr_bg = go.GetComponent<SpriteRenderer>();
                    SavePart = "BACKGROUND!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + sr_bg.color.r + "!" + sr_bg.color.g + "!" + sr_bg.color.b;
                    break;
                case "Level End":
                case "Level End(Clone)":
                    SavePart = "LEVEL_END!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z;
                    break;
                case "Text":
                case "Text(Clone)":
                    TextMesh tm = go.GetComponent<TextMesh>();
                    SavePart = "TEXT!" + p.x + "!" + p.y + "!" + p.z + "!" + r.x + "!" + r.y + "!" + r.z + "!" + s.x + "!" + s.y + "!" + s.z + "!" + tm.text.Replace('!', '§');
                    break;
            }
            if (SaveString == "")
            {
                SaveString = SavePart;
            }
            else
            {
                SaveString += "|" + SavePart;
            }
        }

        string fileName = "LevelAutosave.txt";
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        var sr = File.CreateText(fileName);
        sr.WriteLine(SaveString);
        sr.Close();
        string url = "http://liamlime.com/api/1.0/ld35/submitscore/maps.php";
        WWWForm form = new WWWForm();
        form.AddField("data", SaveString);
        form.AddField("name", Settings.PlayerName);
        form.AddField("levelID", LevelID.ToString());
        WWW www = new WWW(url, form);
    }



    public void SpawnLevel(string level)
    {
        string[] parts = level.Split('|');
        foreach (string part in parts)
        {
            string[] info = part.Split('!');
            if (info.Length > 0)
            {
                GameObject go = null;
                switch (info[0])
                {
                    case "ID":
                        if (!int.TryParse(info[1], out LevelID))
                        {
                            LevelID = Random.Range(1, 99999999);
                        }
                        if(LevelID == 0) {
                            LevelID = Random.Range(1, 99999999);
                        }
                        break;
                    case "BOX":
                    case "CLAW":
                    case "PLANK_RED":
                    case "PLANK_BLUE":
                    case "DOOR_RED":
                    case "DOOR_BLUE":
                    case "TEXT":
                    case "LEVEL_END":
                    case "TERRAIN":
                    case "BACKGROUND":
                        if (info[0] == "BOX")
                        {
                            go = SpawnObjectGO("BOX");
                        }
                        else if (info[0] == "CLAW")
                        {
                            go = SpawnObjectGO("CLAW");
                        }
                        else if (info[0] == "PLANK_RED")
                        {
                            go = SpawnObjectGO("PLANK RED");
                        }
                        else if (info[0] == "PLANK_BLUE")
                        {
                            go = SpawnObjectGO("PLANK BLUE");
                        }
                        else if (info[0] == "DOOR_BLUE")
                        {
                            go = SpawnObjectGO("DOOR BLUE");
                        }
                        else if (info[0] == "DOOR_RED")
                        {
                            go = SpawnObjectGO("DOOR RED");
                        }
                        else if (info[0] == "TEXT")
                        {
                            go = SpawnObjectGO("TEXT");
                        }
                        else if (info[0] == "TERRAIN")
                        {
                            go = SpawnObjectGO("WALL");
                        }
                        else if (info[0] == "BACKGROUND")
                        {
                            go = SpawnObjectGO("BG");
                        }
                        else if (info[0] == "LEVEL_END")
                        {
                            go = SpawnObjectGO("END");
                        }
                        else
                        {
                            go = Instantiate(PF_BOX);
                        }
                        float posx;
                        float posy;
                        float posz;
                        float rotx;
                        float roty;
                        float rotz;
                        float scalex;
                        float scaley;
                        float scalez;
                        if (!float.TryParse(info[1], out posx))
                        {
                            posx = 0;
                        }
                        if (!float.TryParse(info[2], out posy))
                        {
                            posy = 0;
                        }
                        if (!float.TryParse(info[3], out posz))
                        {
                            posz = 0;
                        }
                        if (!float.TryParse(info[4], out rotx))
                        {
                            rotx = 0;
                        }
                        if (!float.TryParse(info[5], out roty))
                        {
                            roty = 0;
                        }
                        if (!float.TryParse(info[6], out rotz))
                        {
                            rotz = 0;
                        }
                        if (!float.TryParse(info[7], out scalex))
                        {
                            scalex = 1;
                        }
                        if (!float.TryParse(info[8], out scaley))
                        {
                            scaley = 1;
                        }
                        if (!float.TryParse(info[9], out scalez))
                        {
                            scalez = 1;
                        }

                        go.transform.SetParent(TerrainPivot.transform);
                        go.transform.localPosition = new Vector3(posx, posy, posz);
                        go.transform.localRotation = Quaternion.Euler(rotx, roty, rotz);
                        go.transform.localScale = new Vector3(scalex, scaley, scalez);
                        break;
                }

                switch (info[0])
                {
                    case "PLANK_RED":
                    case "PLANK_BLUE":
                        if (go != null)
                        {
                            float length = 1;
                            if (!float.TryParse(info[10], out length))
                            {
                                length = 1;
                            }
                            GameObject plank = go.transform.FindChild("Plank").gameObject;
                            GameObject goleftclaw = go.transform.FindChild("Claw Left").gameObject;
                            GameObject gorightclaw = go.transform.FindChild("Claw Right").gameObject;
                            plank.transform.localScale = new Vector3(length, 0.1681681f, 0);
                            goleftclaw.transform.localPosition = new Vector3(-0.3845912f * length, 3.54f, -1);
                            gorightclaw.transform.localPosition = new Vector3(0.3845912f * length, 3.54f, -1);
                        }
                        break;
                    case "TEXT":
                        if (go != null)
                        {
                            TextMesh tm = go.GetComponent<TextMesh>();
                            tm.text = info[10].Replace('§', '!');
                        }
                        break;
                    case "BOX":

                        float r1 = 0;
                        if (!float.TryParse(info[10], out r1))
                        {
                            r1 = 1f;
                        }
                        float g1 = 0;
                        if (!float.TryParse(info[11], out g1))
                        {
                            g1 = 1f;
                        }
                        float b1 = 0;
                        if (!float.TryParse(info[12], out b1))
                        {
                            b1 = 1f;
                        }
                        float r2 = 0;
                        if (!float.TryParse(info[13], out r2))
                        {
                            r2 = 1f;
                        }
                        float g2 = 0;
                        if (!float.TryParse(info[14], out g2))
                        {
                            g2 = 1f;
                        }
                        float b2 = 0;
                        if (!float.TryParse(info[15], out b2))
                        {
                            b2 = 1f;
                        }

                        Color c1 = new Color();
                        c1.r = r1;
                        c1.g = g1;
                        c1.b = b1;
                        c1.a = 1;
                        Color c2 = new Color();
                        c2.r = r2;
                        c2.g = g2;
                        c2.b = b2;
                        c2.a = 1;

                        GameObject go_box_light = go.transform.FindChild("Box Light").gameObject;
                        SpriteRenderer sr_outer = go.GetComponent<SpriteRenderer>();
                        SpriteRenderer sr_inner = go_box_light.GetComponent<SpriteRenderer>();

                        sr_outer.color = c1;
                        sr_inner.color = c2;

                        break;
                    case "CLAW":
                        float cr1 = 0;
                        if (!float.TryParse(info[10], out cr1))
                        {
                            cr1 = 1f;
                        }
                        float cg1 = 0;
                        if (!float.TryParse(info[11], out cg1))
                        {
                            cg1 = 1f;
                        }
                        float cb1 = 0;
                        if (!float.TryParse(info[12], out cb1))
                        {
                            cb1 = 1f;
                        }
                        float cr2 = 0;
                        if (!float.TryParse(info[13], out cr2))
                        {
                            cr2 = 1f;
                        }
                        float cg2 = 0;
                        if (!float.TryParse(info[14], out cg2))
                        {
                            cg2 = 1f;
                        }
                        float cb2 = 0;
                        if (!float.TryParse(info[15], out cb2))
                        {
                            cb2 = 1f;
                        }
                        float cr3 = 0;
                        if (!float.TryParse(info[16], out cr3))
                        {
                            cr3 = 1f;
                        }
                        float cg3 = 0;
                        if (!float.TryParse(info[17], out cg3))
                        {
                            cg3 = 1f;
                        }
                        float cb3 = 0;
                        if (!float.TryParse(info[18], out cb3))
                        {
                            cb3 = 1f;
                        }

                        Color cc1 = new Color();
                        cc1.r = cr1;
                        cc1.g = cg1;
                        cc1.b = cb1;
                        cc1.a = 1;
                        Color cc2 = new Color();
                        cc2.r = cr2;
                        cc2.g = cg2;
                        cc2.b = cb2;
                        cc2.a = 1;
                        Color cc3 = new Color();
                        cc3.r = cr3;
                        cc3.g = cg3;
                        cc3.b = cb3;
                        cc3.a = 1;

                        GameObject go_claw_claw = go.transform.FindChild("Claw").gameObject;
                        GameObject go_claw_o = go.transform.FindChild("Box").gameObject;
                        GameObject go_claw_i = go_claw_o.transform.FindChild("Box Light").gameObject;
                        SpriteRenderer sr_claw = go_claw_claw.GetComponent<SpriteRenderer>();
                        SpriteRenderer sr_claw_o = go_claw_o.GetComponent<SpriteRenderer>();
                        SpriteRenderer sr_claw_i = go_claw_i.GetComponent<SpriteRenderer>();
                        break;
                    case "TERRAIN":
                    case "BACKGROUND":
                        float tr1 = 0;
                        if (!float.TryParse(info[10], out tr1))
                        {
                            tr1 = 1f;
                        }
                        float tg1 = 0;
                        if (!float.TryParse(info[11], out tg1))
                        {
                            tg1 = 1f;
                        }
                        float tb1 = 0;
                        if (!float.TryParse(info[12], out tb1))
                        {
                            tb1 = 1f;
                        }

                        Color tc1 = new Color();
                        tc1.r = tr1;
                        tc1.g = tg1;
                        tc1.b = tb1;
                        tc1.a = 1;

                        SpriteRenderer sr_t = go.GetComponent<SpriteRenderer>();
                        sr_t.color = tc1;
                        break;
                }
            }
        }
    }
}
