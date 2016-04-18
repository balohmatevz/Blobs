using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayBtn : MonoBehaviour
{

    public Text t;
    public Button b;
    int num;

    // Use this for initialization
    void Start()
    {
        b.onClick.AddListener(() =>
        {
            MainMenuController.mmc.OpenPlay(num);
        });
        b.interactable = Settings.PlayedTutorial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Apply(int i)
    {
        t.text=  "Map #"+i;
        num = i;
    }
}
