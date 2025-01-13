using System.Collections;
using UnityEngine;

public class GameBgmManager : MonoBehaviour
{
    public static GameBgmManager Instance;

    public AudioClip initialClip;
    public float fadeDuration = 1.5f;

    private AudioSource bgmSource;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;

            if (initialClip != null)
            {
                bgmSource.clip = initialClip;
                bgmSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeAudioClip(AudioClip newClip)
    {
        if (newClip != null && bgmSource.clip != newClip)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(SwitchAudioClipWithFade(newClip));
        }
    }

    private IEnumerator SwitchAudioClipWithFade(AudioClip newClip)
    {
        if (bgmSource.isPlaying)
        {
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                bgmSource.volume = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        bgmSource.volume = 0.5f;
    }
}