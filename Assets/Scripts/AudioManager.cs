using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Sound {BGM, death, WallTouch, PortalIn, PortalOut, CheckPoint};
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    AudioManager instance;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = audioClips[(int)Sound.BGM];
        musicSource.Play();
    }

    public void PlaySounds(Sound sound)
    {
        sfxSource.PlayOneShot(audioClips[(int)sound]);
    }
}
