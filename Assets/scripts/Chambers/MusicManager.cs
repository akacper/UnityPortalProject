using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public int lastLevel;
    private int sceneCount;

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

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }
}
