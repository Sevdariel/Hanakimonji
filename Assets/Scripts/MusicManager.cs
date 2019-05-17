using UnityEngine;

public class MusicManager : MonoBehaviour 
{
    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Dont destroy on load: " + name);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }
	
    void OnLevelWasLoaded(int level)
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[level];
        Debug.Log("Playing clip: " + thisLevelMusic);

        if (thisLevelMusic) //if thisLevelMusic != NULL
        {
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void ChangeVolume(float volume)
    {
        audioSource.volume = Mathf.Pow(volume, 2f);
    }
}
