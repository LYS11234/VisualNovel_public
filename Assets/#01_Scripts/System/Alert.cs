using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Alert : MonoBehaviour
{
    public Text alertMessege;
    public Button acceptButton;
    public Button cancelButton;

    public string file;

    private void Start()
    {
        cancelButton.onClick.AddListener(DestroyAlert);
    }

    public void LoadActive()
    {
        Debug.Log("버튼에 기능...넣을게...");
        acceptButton.onClick.AddListener(LoadFile);
        Debug.Log("해치웠나?");
    }

    public void DestroyAlert()
    {
        Destroy(gameObject);
    }

    public void LoadFile()
    {
        Debug.Log("해치웠다!");
        string loaddata = File.ReadAllText(file);
        Database.instance.nowPlayer = JsonUtility.FromJson<PlayerData>(loaddata);
        Database.instance.destination = "#02_PlayScene";
        SceneManager.LoadScene("#99_LoadingScene");
    }
}
