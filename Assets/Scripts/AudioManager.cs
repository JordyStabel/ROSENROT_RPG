using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx, backgroundMusic;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < sfx.Length)
        {
            sfx[sfxIndex].Play();
        }
    }

    public void PlayerBackgroundMusic(int musicIndex)
    {
        if (!backgroundMusic[musicIndex].isPlaying)
        {
            StopMusic();
            if (musicIndex < backgroundMusic.Length)
            {
                backgroundMusic[musicIndex].Play();
            }
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource audio in backgroundMusic)
        {
            audio.Stop();
        }
    }
}
