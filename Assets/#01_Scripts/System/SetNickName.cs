using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetNickName : MonoBehaviour
{
    InputField inputField;
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    

    public void SetPlayerName()
    {
        Database.instance.nowPlayer = new PlayerData();
        if (inputField.text.Length > 0)
        {
            Database.instance.nowPlayer.playerName = inputField.text;
            Database.instance.destination = "#02_PlayScene";
            SceneManager.LoadScene("#99_LoadingScene");
            Time.timeScale = 1;
        }
    }
}
