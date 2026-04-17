using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public CanvasManager canvasManager;
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        Database.instance.nowPlayer = null;
        Time.timeScale = 1;
        Database.instance.destination = "#01_LobbyScene";
        SceneManager.LoadScene("#99_LoadingScene");
        SoundManager.instance.bgm_.Stop();
        SoundManager.instance.sfx_.Stop();
        SoundManager.instance.voice_.Stop();
    }


    private void OnDestroy()
    { 
        Time.timeScale = 1;
    }
}
