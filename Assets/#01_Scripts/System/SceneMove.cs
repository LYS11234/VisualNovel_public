using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    private string prefabName;
    GameObject obj;
    public void ShowPrefab()
    {
        Time.timeScale = 0;
        obj = Resources.Load<GameObject>("Prefabs/" + prefabName);
        SoundManager.instance.bgm_.Pause();
        SoundManager.instance.voice_.Pause();
        Instantiate(obj);
    }


    public void MoveScene()
    {
        Database.instance.destination = prefabName;
        SceneManager.LoadScene("#99_LoadingScene");
        SoundManager.instance.bgm_.Stop();
    }


}
