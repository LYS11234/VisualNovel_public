using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData
{
    public string playerName;
    #region Player Info
    public byte grade = 1;
    public byte[] score;
    public byte month = 3;
    public byte date = 2;

    public byte strength = 20;
    public byte intelligence = 20;
    public byte selfEsteem = 40;

    public string dialogueFile = "Scripts";
    public int dialogueNum;
    public string backgroundImg = "#000_HomeEnterance.jpg";
    #endregion

    #region First
    public int affection_first;
    public bool firstClear;

    #endregion

    #region Second
    public int affection_second;
    public bool secondClear;

    #endregion

    #region Third
    public int affection_third;
    public bool thirdClear;

    #endregion
} //Player Data

public class UserSettings
{
    public float master;
    public float bgm;
    public float sfx;
    public float voice;

    public bool masterMute;
    public bool bgmMute;
    public bool sfxMute;
    public bool voiceMute;


 
} //Settings about Game

public class UserData
{


    //public string language;


    #region CG
    public bool[] cg = new bool[20];
    #endregion
} 

public class Database : MonoBehaviour
{
    public static Database instance;
    private void Awake()
    {
        
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }

    public PlayerData nowPlayer = new PlayerData();
    public UserSettings userSettings = new UserSettings();  
    public UserData userData = new UserData();
    

    public string destination;


    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/save/UserSetting/Settings"))
        { 
            string loaddata = File.ReadAllText(Application.persistentDataPath + "/save/UserSetting/Settings");
            userSettings = JsonUtility.FromJson<UserSettings>(loaddata);
        }
        if (File.Exists(Application.persistentDataPath + "/save/UserData/UserData.dat"))
        {
            string encodedData = File.ReadAllText(Application.persistentDataPath + "/save/UserData/UserData.dat");
            byte[] bytes0 = System.Convert.FromBase64String(encodedData);
            string decodedData0 = System.Text.Encoding.BigEndianUnicode.GetString(bytes0);
            byte[] bytes1 = System.Convert.FromBase64String(decodedData0);
            string decodedData1 = System.Text.Encoding.Unicode.GetString(bytes1);
            byte[] bytes = System.Convert.FromBase64String(decodedData1);
            string loadData = System.Text.Encoding.UTF8.GetString(bytes);
            userData = JsonUtility.FromJson<UserData>(loadData);
        }
    }



}
