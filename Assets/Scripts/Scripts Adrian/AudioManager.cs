using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSO audioSettings;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Image soundImage;
    private float volume;
    private bool isMuted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        isMuted = false;
        LoadVolume();
    }
    private void LoadVolume()
    {
        if(sliderMaster && sliderMusic && sliderSFX != null)
        {
            sliderMaster.value = audioSettings.masterVolume;
            sliderMusic.value = audioSettings.musicVolume;
            sliderSFX.value = audioSettings.sfxVolume;
        }
    }
    public void SetVolumeMaster()
    {
        if(sliderMaster != null)
        {
            volume = sliderMaster.value;
            myMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            audioSettings.masterVolume = volume;
        }   
    }
    public void SetVolumeMusic()
    {
        if(sliderMusic != null)
        {
            volume = sliderMusic.value;
            myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            audioSettings.musicVolume = volume;
        }      
    }
    public void SetVolumeSfx()
    {
        if(sliderSFX != null)
        {
            volume = sliderSFX.value;
            myMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
            audioSettings.sfxVolume = volume;
        }      
    }
    public void ChangeSound()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            soundImage.sprite = soundOff;
            myMixer.SetFloat("MasterVolume", -80f);
            myMixer.SetFloat("MusicVolume", -80f);
            myMixer.SetFloat("SfxVolume", -80f);
        }
        else
        {
            soundImage.sprite= soundOn;

            myMixer.SetFloat("MasterVolume", audioSettings.masterVolume);
            myMixer.SetFloat("MusicVolume", audioSettings.musicVolume);
            myMixer.SetFloat("SfxVolume", audioSettings.sfxVolume);
        }
    }
}
