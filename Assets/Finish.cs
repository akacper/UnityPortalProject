using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    public int currentScene = 1;
    public int minplaced = 2;
    public int minpasses = 1;

    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI portalsPlaced;
    public TextMeshProUGUI portalsPassed;
    public TextMeshProUGUI timePassed;
    public Button nextLevel;
    private float elapsedTime = 0f;

    void Start()
    {
        // Disable the UI elements initially
        levelCompleteText.gameObject.SetActive(false);
        portalsPlaced.gameObject.SetActive(false);
        portalsPassed.gameObject.SetActive(false);
        timePassed.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (levelCompleteText.gameObject.activeSelf)
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
        if (!levelCompleteText.gameObject.activeSelf)
        {
            portalsPlaced.text += placed + "/" + minplaced;
            portalsPassed.text += passes + "/" + minpasses;
            timePassed.text += Mathf.Round(elapsedTime) + " s";
        }
        levelCompleteText.gameObject.SetActive(true);
        portalsPlaced.gameObject.SetActive(true);
        portalsPassed.gameObject.SetActive(true);
        timePassed.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
