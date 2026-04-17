using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion
    public AudioMixer audioMixer;

    public AudioMixerGroup master;
    public AudioMixerGroup bgm;
    public AudioMixerGroup sfx;
    public AudioMixerGroup voice;

    public AudioSource bgm_;
    public AudioSource sfx_;
    public AudioSource voice_;
    private AudioSource click_;

    [SerializeField]
    private AudioClip clickSound;


    private void Start()
    {
        if(!Database.instance.userSettings.masterMute)
            audioMixer.SetFloat("Master", Database.instance.userSettings.master);
        else
            audioMixer.SetFloat("Master", -80);
        if (!Database.instance.userSettings.bgmMute)
            audioMixer.SetFloat("BGM", Database.instance.userSettings.bgm);
        else
            audioMixer.SetFloat("BGM", -80);
        if (!Database.instance.userSettings.sfxMute)
            audioMixer.SetFloat("SFX", Database.instance.userSettings.sfx);
        else
            audioMixer.SetFloat("SFX", -80);
        if (!Database.instance.userSettings.voiceMute)
            audioMixer.SetFloat("Voice", Database.instance.userSettings.voice);
        else
            audioMixer.SetFloat("Voice", -80);
        bgm_ = GetComponentsInChildren<AudioSource>()[1];
        bgm_.outputAudioMixerGroup = bgm;
        sfx_ = GetComponentsInChildren<AudioSource>()[2];
        sfx_.outputAudioMixerGroup = sfx;
        voice_ = GetComponentsInChildren<AudioSource>()[3];
        voice_.outputAudioMixerGroup = voice;
        click_ = GetComponentsInChildren<AudioSource>()[0];
        click_.outputAudioMixerGroup = sfx;
        click_.clip = clickSound;

    }

    private void Update()
    {
        if (!Database.instance.userSettings.masterMute || !Database.instance.userSettings.sfxMute)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                click_.Play();
            }
        }
    }
}
