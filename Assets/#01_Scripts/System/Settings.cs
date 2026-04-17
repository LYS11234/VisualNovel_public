using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Slider master;
    [SerializeField]
    private Slider bgm;
    [SerializeField]
    private Slider sfx;
    [SerializeField]
    private Slider voice;
    [SerializeField]
    private Toggle masterToggle;
    [SerializeField]
    private Toggle bgmToggle;
    [SerializeField]
    private Toggle sfxToggle;
    [SerializeField]
    private Toggle voiceToggle;


    private void Start()
    {
        SoundManager.instance.audioMixer.GetFloat("Master", out float currentMaster);
        if(Database.instance.userSettings.masterMute)
            currentMaster = Database.instance.userSettings.master;

        master.value = currentMaster;
        master.gameObject.GetComponentInChildren<Text>().text = (((master.value + 60) / (60)) * 100).ToString("00") + "%";
        masterToggle.isOn = Database.instance.userSettings.masterMute;
        SoundManager.instance.audioMixer.GetFloat("BGM", out float currentBGM);
        if (Database.instance.userSettings.bgmMute)
            currentBGM = Database.instance.userSettings.bgm;
        bgm.value = currentBGM;
        bgm.gameObject.GetComponentInChildren<Text>().text = (((bgm.value + 60) / (60)) * 100).ToString("00") + "%";
        bgmToggle.isOn = Database.instance.userSettings.bgmMute;
        SoundManager.instance.audioMixer.GetFloat("SFX", out float currentSFX);
        if(Database.instance.userSettings.sfxMute)
            currentSFX = Database.instance.userSettings.sfx;
        sfx.value = currentSFX;
        sfx.gameObject.GetComponentInChildren<Text>().text = (((sfx.value + 60) / (60)) * 100).ToString("00") + "%";
        sfxToggle.isOn = Database.instance.userSettings.sfxMute;
        SoundManager.instance.audioMixer.GetFloat("Voice", out float currentVoice);
        if (Database.instance.userSettings.voiceMute)
            currentVoice = Database.instance.userSettings.voice;
        voice.value = currentVoice;
        voice.gameObject.GetComponentInChildren<Text>().text = (((voice.value + 60) / (60)) * 100).ToString("00") + "%";
        voiceToggle.isOn = Database.instance.userSettings.voiceMute;
    }

    public void SetMasterVol()
    {
        if(!Database.instance.userSettings.masterMute)
            SoundManager.instance.audioMixer.SetFloat("Master", master.value);
        Database.instance.userSettings.master = master.value;
        Text text = master.gameObject.GetComponentInChildren<Text>();
        text.text = (((master.value + 60) / (60)) * 100).ToString("00") + "%";
        SaveSettings();
    }

    public void SetBGMVol()
    {
        if(!Database.instance.userSettings.bgmMute)
            SoundManager.instance.audioMixer.SetFloat("BGM", bgm.value);
        Database.instance.userSettings.bgm = bgm.value;
        Text text = bgm.gameObject.GetComponentInChildren<Text>();
        text.text = (((bgm.value + 60) / (60)) * 100).ToString("00") + "%";
        SaveSettings();
    }

    public void SetSFXVol()
    {
        if(!Database.instance.userSettings.sfxMute)
            SoundManager.instance.audioMixer.SetFloat("SFX", sfx.value);
        Database.instance.userSettings.sfx = sfx.value;
        Text text = sfx.gameObject.GetComponentInChildren<Text>();
        text.text = (((sfx.value + 60) / (60)) * 100).ToString("00") + "%";
        SaveSettings();
    }

    public void SetVoiceVol()
    {
        if(!Database.instance.userSettings.voiceMute)
            SoundManager.instance.audioMixer.SetFloat("Voice", voice.value);
        Database.instance.userSettings.voice = voice.value;
        Text text = voice.gameObject.GetComponentInChildren<Text>();
        text.text = (((voice.value + 60) / (60)) * 100).ToString("00") + "%";
        SaveSettings();
    }

    public void SaveSettings()
    {
        string path = Path.Combine(Application.persistentDataPath + "/save/UserSetting");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string saveData = JsonUtility.ToJson(Database.instance.userSettings);
        File.WriteAllText(Application.persistentDataPath + "/save/UserSetting/Settings", saveData);
    }

    public void MuteMaster()
    {
        Database.instance.userSettings.masterMute = masterToggle.isOn;
        if(masterToggle.isOn)
            SoundManager.instance.audioMixer.SetFloat("Master", -80);
        else
            SoundManager.instance.audioMixer.SetFloat("Master", master.value);
        SaveSettings();
    }

    public void MuteBGM()
    {
        Database.instance.userSettings.bgmMute = bgmToggle.isOn;
        if (bgmToggle.isOn)
            SoundManager.instance.audioMixer.SetFloat("BGM", -80);
        else
            SoundManager.instance.audioMixer.SetFloat("BGM", bgm.value);
        SaveSettings();
    }

    public void MuteSFX()
    {
        Database.instance.userSettings.sfxMute = sfxToggle.isOn;
        if (sfxToggle.isOn)
            SoundManager.instance.audioMixer.SetFloat("SFX", -80);
        else
            SoundManager.instance.audioMixer.SetFloat("SFX", sfx.value);
        SaveSettings();
    }

    public void MuteVoice()
    {
        Database.instance.userSettings.voiceMute = voiceToggle.isOn;
        if (voiceToggle.isOn)
            SoundManager.instance.audioMixer.SetFloat("Voice", -80);
        else
            SoundManager.instance.audioMixer.SetFloat("Voice", voice.value);
        SaveSettings();
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
