using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    public int minplaced = 2;
    public int minpasses = 1;
    public int currentScene = 1;
    private int sceneCount;
    public GameObject FinishUI;
    public TextMeshProUGUI portalsPlaced;
    public TextMeshProUGUI portalsPassed;
    public TextMeshProUGUI timePassed;
    public Button nextLevel;
    public GameObject musicManager;
    private float elapsedTime = 0f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip finishSound;

    void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        currentScene = SceneManager.GetActiveScene().buildIndex;     
        // Disable the UI elements initially
        FinishUI.SetActive(false);
        audioSource = FinishUI.GetComponent<AudioSource>();
        audioSource.clip = finishSound;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (FinishUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoNextLevel();
            }
        }
    }

    public void ShowUI(int placed, int passes)
    {
        // Enable the UI elements and update the score text
        if (!FinishUI.activeSelf)
        {
            portalsPlaced.text += placed + "/" + minplaced;
            portalsPassed.text += passes + "/" + minpasses;
            timePassed.text += Mathf.Round(elapsedTime) + " s";
            PlayerPrefs.SetInt("LastLevel", currentScene+1);
            PlayerPrefs.Save();
        }
        FinishUI.SetActive(true);
        audioSource.Play();
        if (currentScene == sceneCount-1)
        {
            nextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "Menu";
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void GoNextLevel()
    {
        // Time.timeScale = 1f;
        if (currentScene < sceneCount-1)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Destroy(musicManager);
            SceneManager.LoadScene(0);
        }
    }
}
