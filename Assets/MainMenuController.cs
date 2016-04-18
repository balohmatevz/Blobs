using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{

    const float MENU_FADE_SPEED = 3;
    public static MainMenuController mmc;

    public CanvasGroup MenuElementsCG;
    public CanvasGroup MenuMainCG;
    public CanvasGroup MenuPlayCG;
    public CanvasGroup MenuEditCG;
    public CanvasGroup MenuSettingsCG;
    public CanvasGroup MenuCreditsCG;
    public CanvasGroup MenuExitCG;
    public Button EditorButton;

    float delay = 0.5f;
    int OpenMenu = -1;
    CanvasGroup OpeningMenu;
    bool MenuOpening = false;
    bool MenuClosing = false;
    bool OpeningPlay = false;

    public Slider SettingsMusic;
    public Slider SettingsSound;
    public InputField PlayerNameInput;
    public AudioSource MainMenuMusic;

    public RectTransform EditorListContainer;
    public GameObject PF_EditorListEntry;
    public RectTransform PlayListContainer;
    public GameObject PF_PlayListEntry;

    void Awake()
    {
        mmc = this;
    }

    // Use this for initialization
    void Start()
    {
        Settings.LoadSettings();
        SettingsMusic.value = Settings.VolumeMusic;
        SettingsSound.value = Settings.VolumeSound;
        PlayerNameInput.text = Settings.PlayerName;
        MenuElementsCG.alpha = 0;
        MenuMainCG.alpha = 0;
        MenuPlayCG.alpha = 0;
        MenuEditCG.alpha = 0;
        MenuSettingsCG.alpha = 0;
        MenuCreditsCG.alpha = 0;
        MenuExitCG.alpha = 0;

        MenuPlayCG.gameObject.SetActive(false);
        MenuEditCG.gameObject.SetActive(false);
        MenuSettingsCG.gameObject.SetActive(false);
        MenuCreditsCG.gameObject.SetActive(false);
        MenuExitCG.gameObject.SetActive(false);

        MenuMainCG.gameObject.SetActive(true);
        MenuOpening = true;
        OpeningMenu = MenuMainCG;
        OpenMenu = 0;
        Settings.GetMapList();
        int count = 0;
        foreach (int i in Settings.mapList.Keys)
        {
            GameObject go = Instantiate(PF_EditorListEntry);
            go.transform.SetParent(EditorListContainer);
            go.transform.localPosition = new Vector3(0, count * 30 + 50, 0);
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            EditorBtn b = go.GetComponent<EditorBtn>();
            b.Apply(i);
            count++;
        }
        count = 0;
        foreach (int i in Settings.mapList.Keys)
        {
            GameObject go = Instantiate(PF_PlayListEntry);
            go.transform.SetParent(PlayListContainer);
            go.transform.localPosition = new Vector3(0, count * 30 + 50, 0);
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            PlayBtn b = go.GetComponent<PlayBtn>();
            b.Apply(i);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < -10)
        {
            delay = -10;
        }
        if (delay > 0)
        {
            return;
        }

        if (OpeningPlay)
        {
            MenuElementsCG.alpha -= Time.deltaTime;
            MenuPlayCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
            if (MenuElementsCG.alpha <= 0.01f)
            {
                if (Settings.mapid == 0)
                {
                    SceneManager.LoadScene("cutscene");
                }
                else
                {
                    SceneManager.LoadScene("play");
                }
            }
            return;
        }

        MenuElementsCG.alpha += Time.deltaTime;


        if (MenuElementsCG.alpha < 0.99f)
        {
            return;
        }

        if (MenuOpening)
        {
            if (MenuClosing)
            {
                MenuMainCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                MenuPlayCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                MenuEditCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                MenuSettingsCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                MenuCreditsCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                MenuExitCG.alpha -= MENU_FADE_SPEED * Time.deltaTime;
                if (MenuMainCG.alpha <= 0.01f && MenuPlayCG.alpha <= 0.01f && MenuEditCG.alpha <= 0.01f && MenuSettingsCG.alpha <= 0.01f && MenuCreditsCG.alpha <= 0.01f && MenuExitCG.alpha <= 0.01f)
                {
                    MenuClosing = false;
                    MenuMainCG.gameObject.SetActive(false);
                    MenuPlayCG.gameObject.SetActive(false);
                    MenuEditCG.gameObject.SetActive(false);
                    MenuSettingsCG.gameObject.SetActive(false);
                    MenuCreditsCG.gameObject.SetActive(false);
                    MenuExitCG.gameObject.SetActive(false);

                    OpeningMenu.gameObject.SetActive(true);
                }
            }
            else
            {
                OpeningMenu.alpha += MENU_FADE_SPEED * Time.deltaTime;
                if (OpeningMenu.alpha >= 1)
                {
                    MenuOpening = false;
                }
            }
        }
    }

    public void MenuOpen(int menu)
    {
        if (OpenMenu == menu)
        {
            return;
        }

        OpenMenu = menu;
        MenuOpening = true;
        MenuClosing = true;
        switch (menu)
        {
            case 0:
                OpeningMenu = MenuMainCG;
                break;
            case 1:
                OpeningMenu = MenuPlayCG;
                break;
            case 2:
                OpeningMenu = MenuSettingsCG;
                break;
            case 3:
                OpeningMenu = MenuCreditsCG;
                break;
            case 4:
                OpeningMenu = MenuExitCG;
                break;
            case 5:
                OpeningMenu = MenuEditCG;
                break;
        }
        OpeningMenu.gameObject.SetActive(true);
    }




    public void INPUT_URLRateEntry()
    {
        Application.OpenURL("http://ludumdare.com/compo/author/liamlime/");
    }

    public void INPUT_URLLiamLime()
    {
        Application.OpenURL("http://liamlime.com/");
    }

    public void INPUT_URLLudumDare()
    {
        Application.OpenURL("http://ludumdare.com/");
    }

    public void INPUT_Quit()
    {
        Application.Quit();
    }

    public void ApplySetings()
    {
        MainMenuMusic.volume = Settings.VolumeMusic;
        EditorButton.interactable = Settings.PlayedTutorial;
    }

    public void SLIDER_AudioMusic()
    {
        Settings.VolumeMusic = SettingsMusic.value;
        Settings.SaveSettings();
        ApplySetings();
    }

    public void SLIDER_AudioSound()
    {
        Settings.VolumeSound = SettingsSound.value;
        Settings.SaveSettings();
        ApplySetings();
    }

    public void SLIDER_PlayerName()
    {
        Settings.PlayerName = PlayerNameInput.text;
        Settings.SaveSettings();
        ApplySetings();
    }

    public void OpenEditor(int id)
    {
        Settings.mapid = id;
        SceneManager.LoadScene("edit");
    }

    public void OpenPlay(int id)
    {
        Debug.Log("playing: " + id);
        Settings.mapid = id;
        if (OpeningMenu == MenuPlayCG)
        {
            OpeningPlay = true;
        }
    }

}
