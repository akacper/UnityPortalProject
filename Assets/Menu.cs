using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip clickClip;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = musicClip;
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = clickClip;
        audioSource1.Play();
    }
    public void PlayGame()
    {
        PlaySoundAndWait();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        PlaySoundAndWait();
        Application.Quit();
    }

    IEnumerator PlaySoundAndWait()
    {
        audioSource2.Play();
        while (audioSource2.isPlaying)
        {
            yield return null;
        }
    }
}

