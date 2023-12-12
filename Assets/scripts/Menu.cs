using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int lastLevel;
    private int sceneCount;
    private AudioSource audioSource1;
    [SerializeField] private Button continueButton;
    [SerializeField] private AudioClip musicClip;
    private AudioSource audioSource2;
    [SerializeField] private AudioClip clickClip;
    void Start()
    {
        Time.timeScale = 1f;
        lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        if (lastLevel > 1)
        {
            continueButton.interactable = true;
        }
        sceneCount = SceneManager.sceneCountInBuildSettings;
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = musicClip;
        audioSource1.loop = true;
        audioSource1.Play();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = clickClip;
    }
    public void PlayGame()
    {
        StartCoroutine(NewGameCor());
    }

    public void ContinueGame()
    {
        StartCoroutine(ContinueGameCor());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator NewGameCor()
    {
        audioSource2.Play();
        yield return new WaitForSeconds(audioSource2.clip.length);
        PlayerPrefs.SetInt("LastLevel", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    IEnumerator ContinueGameCor()
    {
        audioSource2.Play();
        yield return new WaitForSeconds(audioSource2.clip.length);
        if (lastLevel == sceneCount-1)
        {
            SceneManager.LoadScene(lastLevel);
        }
        else
        {
            SceneManager.LoadScene(lastLevel+1);
        }
    }
}

