using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class LoadData : MonoBehaviour
{
    public string num;
    private string path;
    [SerializeField]
    private Sprite nullImage;

    private Text text;


    private void Start()
    {
        path = Application.persistentDataPath + "/save/";
        text = GetComponentInChildren<Text>();

        num = gameObject.name;
        if(File.Exists(path + num + ".dat"))
        {
            if (num == "0")
            {
                text.text = "Quick Save: "; 
            }
            else
            {
                text.text = "Save" + num + ": ";
            }
            string file = path + num +".dat";
            var info = new FileInfo(file);
            text.text += info.LastWriteTime.Year.ToString() + "/" + info.LastWriteTime.Month.ToString() + "/" + info.LastWriteTime.Day.ToString()
                + " " + info.LastWriteTime.Hour.ToString() +":" + info.LastWriteTime.Minute.ToString() +":" + info.LastWriteTime.Second.ToString();
            GetComponent<Image>().color = Color.white;
            
        }
        else
        {
            if (num == "0")
            {
                text.text = "Quick Save: No Data";
            }
            else
                text.text = "Save" + num + ": No Data";

            GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            GetComponent<Image>().sprite = nullImage;
        }

    }

    public void LoadSlot()
    {
        if (File.Exists(path + num + ".dat"))
        {
            string encodedData = File.ReadAllText(path + num + ".dat");
            byte[] bytes0 = System.Convert.FromBase64String(encodedData);
            string decodedData0 = System.Text.Encoding.BigEndianUnicode.GetString(bytes0);
            byte[] bytes1 = System.Convert.FromBase64String(decodedData0);
            string decodedData1 = System.Text.Encoding.Unicode.GetString(bytes1);
            byte[] bytes = System.Convert.FromBase64String(decodedData1);
            string loadData = System.Text.Encoding.UTF8.GetString(bytes);
            Database.instance.nowPlayer = JsonUtility.FromJson<PlayerData>(loadData);
            Debug.LogWarning(loadData);
            Time.timeScale = 1;
            Database.instance.destination = "#02_PlayScene";
            SceneManager.LoadScene("#99_LoadingScene");
        }
        else
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Alert");
            obj.GetComponent<Alert>().acceptButton.gameObject.SetActive(false);
            obj.GetComponent<Alert>().cancelButton.GetComponentInChildren<Text>().text = "OK";
            obj.GetComponent<Alert>().alertMessege.text = "No data";
            Instantiate(obj);
        }

    }

    


    public void SaveSlot()
    {
        string saveData = JsonUtility.ToJson(Database.instance.nowPlayer);
        byte[] bytes0 = System.Text.Encoding.UTF8.GetBytes(saveData);
        string encodedBytes0 = System.Convert.ToBase64String(bytes0);
        byte[] bytes2 = System.Text.Encoding.Unicode.GetBytes(encodedBytes0);
        string encodedBytes2 = System.Convert.ToBase64String(bytes2);
        byte[] bytes3 = System.Text.Encoding.BigEndianUnicode.GetBytes(encodedBytes2);
        string encodedData = System.Convert.ToBase64String(bytes3);
        File.WriteAllText(path + num + ".dat", encodedData);
        print(path + num);
        if (num == "0")
        {
            text.text = "Quick Save: " + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString()
                + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        }
        else
            text.text = "Save" + num + ": " + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString()
                + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        GetComponent<Image>().color = Color.white;
        Debug.Log(saveData);
    }

}
