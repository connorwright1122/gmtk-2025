using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    
    // Static instance of the class
    private static SoundFXManager _instance;

    [SerializeField]
    private AudioSource soundFXObject;

    //public AudioClip[] musicClips;
    public AudioClip[] soundFXClips;

    public AudioSource musicSource;
    public float duration = 2f; // Duration over which to fade out the volume


    // Public property to access the instance
    public static SoundFXManager Instance
    {
        get
        {
            // If the instance is null, find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundFXManager>();

                // If still null, create a new GameObject and attach this script
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("SoundFXManager");
                    _instance = singleton.AddComponent<SoundFXManager>();
                }
            }

            return _instance;
        }
    }

    // Ensure the instance is not destroyed when loading new scenes
    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this object
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundFXClipUI()
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);

        audioSource.clip = soundFXClips[0];
        audioSource.volume = 100;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }


    public void StartFadeOut()
    {
        StartCoroutine(FadeOutVolume());
    }

    private IEnumerator FadeOutVolume()
    {
        float startVolume = musicSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / duration);
            yield return null;
        }

        // Ensure the volume is set to 0 at the end
        musicSource.volume = 0f;
    }


}


