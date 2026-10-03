using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Sound {BGM, death, WallTouch, PortalIn, PortalOut, CheckPoint};
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    static AudioManager instance;
    private void Awake() {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
