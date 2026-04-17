using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuickSave : MonoBehaviour
{
    public void Save()
    {
        string saveData = JsonUtility.ToJson(Database.instance.nowPlayer);
        Debug.LogWarning(saveData);
        byte[] bytes0 = System.Text.Encoding.UTF8.GetBytes(saveData);
        string encodedBytes0 = System.Convert.ToBase64String(bytes0);
        Debug.LogWarning(encodedBytes0);
        byte[] bytes1 = System.Text.Encoding.Unicode.GetBytes(encodedBytes0);
        string encodedBytes1 = System.Convert.ToBase64String(bytes1);
        Debug.LogWarning(encodedBytes1);
        byte[] bytes2 = System.Text.Encoding.BigEndianUnicode.GetBytes(encodedBytes1);
        string encodedData = System.Convert.ToBase64String(bytes2);
        Debug.LogWarning(encodedData);
        File.WriteAllText(Application.persistentDataPath + "/save/0.dat", encodedData);
        GameObject obj = Resources.Load<GameObject>("Prefabs/Alert");
        obj.GetComponent<Alert>().acceptButton.gameObject.SetActive(false);
        obj.GetComponent<Alert>().cancelButton.GetComponentInChildren<Text>().text = "OK";
        obj.GetComponent<Alert>().alertMessege.text = "Quick Saved";
        Instantiate(obj);
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save/0.dat"))
        {
            string encodedData = File.ReadAllText(Application.persistentDataPath + "/save/0.dat");
            byte[] bytes0 = System.Convert.FromBase64String(encodedData);
            string decodedData0 = System.Text.Encoding.BigEndianUnicode.GetString(bytes0);
            byte[] bytes1 = System.Convert.FromBase64String(decodedData0);
            string decodedData1 = System.Text.Encoding.Unicode.GetString(bytes1);
            byte[] bytes = System.Convert.FromBase64String(decodedData1);
            string loadData = System.Text.Encoding.UTF8.GetString(bytes);
            Database.instance.nowPlayer = JsonUtility.FromJson<PlayerData>(loadData);
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
}
