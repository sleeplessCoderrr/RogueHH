using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Audio Sources")] 
    [SerializeField] private AudioSource currentBgm;
    [SerializeField] private AudioSource currentSfx;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] gameSfxClips;
    [SerializeField] private AudioClip[] uiSfxClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        currentBgm = gameObject.AddComponent<AudioSource>();
        currentSfx = gameObject.AddComponent<AudioSource>();
    }

    private void Play()
    {
        currentBgm.loop = true;
        currentBgm.Play();
    }

    public void PlayMenuBgm()
    {
        currentBgm.clip = bgmClips[0];
        Play();
    }

    public void PlayUpgradeBgm()
    {
        currentBgm.clip = bgmClips[1];
        Play();
    }

    public void PlayGameBgm()
    {
        currentBgm.clip = bgmClips[2];
        Play();
    }

    public void StopMusic()
    {
        if (currentBgm.isPlaying)
        {
            currentBgm.Stop();
        }
    }

    public void PlayUISfx(int index)
    {
        currentSfx.PlayOneShot(uiSfxClips[index]);
    }

    public void StopSfx()
    {
        if (currentSfx.isPlaying)
        {
            currentSfx.Stop();
        }
    }
}