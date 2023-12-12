using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public int currentScene;
    private bool paused = true;
    public GameObject pauseUI;
    public GameObject musicManager;

    void Start()
    {
        Time.timeScale = 1f;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastLevel", currentScene);
        PlayerPrefs.Save();
        SwitchUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchUI();
        }
        if (musicManager == null)
        {
            musicManager = GameObject.Find("MusicManager");
        }   
    }

    public void SwitchUI()
    {
        paused = !paused;
        pauseUI.SetActive(paused);
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
        if (musicManager != null)
        {
            if (paused)
            {
                musicManager.GetComponent<AudioSource>().Pause();
            }
            else
            {
                musicManager.GetComponent<AudioSource>().UnPause();
            }
        }
        Time.timeScale = paused ? 0f : 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        if (musicManager != null)
        {
            Destroy(musicManager);
        }
        SceneManager.LoadScene(currentScene);
    }
}
