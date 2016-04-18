using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Settings
{

    public static float VolumeMusic = 1;
    public static float VolumeSound = 1;
    public static string PlayerName = "Player";
    static bool Initialized = false;

    public static int mapid;
    public static Dictionary<int, string> mapList;

    public static bool PlayedTutorial = false;

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("VolumeMusic", VolumeMusic);
        PlayerPrefs.SetFloat("VolumeSound", VolumeSound);
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetInt("PlayedTutorial", ((PlayedTutorial) ? 1 : 0));
    }

    public static void LoadSettings()
    {
        if (!Initialized)
        {
            VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic", 1);
            VolumeSound = PlayerPrefs.GetFloat("VolumeSound", 1);
            PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
            int PlayedTutorialInt = PlayerPrefs.GetInt("PlayedTutorial", 0);
            PlayedTutorial = PlayedTutorialInt == 1;
            Initialized = true;
            GetMapList();
        }
    }

    public static void ForceLoadSettings()
    {
        VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic", 1);
        VolumeSound = PlayerPrefs.GetFloat("VolumeSound", 1);
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
        int PlayedTutorialInt = PlayerPrefs.GetInt("PlayedTutorial", 0);
        PlayedTutorial = PlayedTutorialInt == 1;
        Initialized = true;
        GetMapList();
    }

    public static void GetMapList()
    {
        mapList = new Dictionary<int, string>();
        string url = "http://liamlime.com/api/1.0/ld35/submitscore/maps.php";
        WWW www = new WWW(url);

        //float timeout = 1000;
        while(!www.isDone) {
            
        }

        string parsemaps = www.text;
        if (parsemaps.Length > 0)
        {
            string[] lines = parsemaps.Split('\n');
            foreach (string line in lines)
            {
                string id = "";
                if (line.Length > 0)
                {
                    string[] info = line.Split('|');
                    if (info.Length > 0)
                    {
                        string[] idinfo = info[0].Split('!');
                        id = idinfo[1];
                        mapList.Add(int.Parse(id), line);
                    }
                }
            }
        }
    }
}

