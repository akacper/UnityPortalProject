using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    // Make sure the instance is not destroyed when loading a new scene
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add your music playing logic here
    void Start()
    {
        // For example, you might use AudioSource to play music
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
