using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumnSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolumn"))
        {
            LoadVolumnSeetings();
            LoadSFXVolumnSettings();
        }
        else
        {
            SetMusicVolumn();
            SetSFXVolumn();
        }
    }
    public void SetMusicVolumn()
    {
        float volumn = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volumn)*20);
        PlayerPrefs.SetFloat("MusicVolumn", volumn);
    }
    public void SetSFXVolumn()
    {
        float volumn = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volumn)*20);
        PlayerPrefs.SetFloat("SFXVolumn", volumn);
    }
    void LoadVolumnSeetings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumn", 1f);
        SetMusicVolumn();
    }
    void LoadSFXVolumnSettings()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumn", 1f);
        SetSFXVolumn();
    }

}
