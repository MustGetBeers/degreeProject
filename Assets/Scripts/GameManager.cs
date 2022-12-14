using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject timerPanel;
    public GameObject endingPanel;
    public GameObject highScoreValue;
    public bool showMenu = true;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManagerIsNull");

            }
            return _instance;
        }
    }


    // Start is called before the first frame update

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
        _instance = this;
        DontDestroyOnLoad(this);

    }
    void Start()
    {
        if (showMenu == true)
        {
            ShowMainMenu();
        }
        else
        {
            StartGame();
        }


    }


    //update canvas etc.
    void ShowMainMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
        TextMeshProUGUI scoreValue = highScoreValue.GetComponent<TextMeshProUGUI>();
        scoreValue.text = "1000".ToString();
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        timerPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void Restarting()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restarting");
        showMenu = false;

    }

}
