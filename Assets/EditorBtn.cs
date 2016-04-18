using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditorBtn : MonoBehaviour
{

    public Text t;
    public Button b;
    int num;

    // Use this for initialization
    void Start()
    {
        b.onClick.AddListener(() =>
        {
            MainMenuController.mmc.OpenEditor(num);
        });
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
