using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerSOS playerScore;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text distanceTXT;
    [SerializeField] private GameObject panelInput;
    [SerializeField] private GameObject panelAudio;
    [SerializeField] private Animator controlsAnimator;
    [SerializeField] private Image pauseButton;
    [SerializeField] private GameObject panelPause;
    private float distance;
    private float finalDistance = 0f;
    private bool gameOver = false;
    private bool isGamePaused = false;
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);

        }
    }
    private void Update()
    {
        UpdateDistance();
    }

    public void ChangePause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        if (!gameOver) 
        {
            gameOver = true;
            ShowPanelResults();
        }
    }
    private void ShowPanelResults()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

        }
    }
    public void ResetGame()
    {
        gameOver = false;
        if(playerScore != null)
        {
            playerScore.ResetScore();
        }
        gameOverPanel.SetActive(false);
    }
    private void UpdateDistance()
    {
        finalDistance = finalDistance + Time.deltaTime * 10f;
        distance = finalDistance;
        if(distanceTXT != null )
        {
            distanceTXT.text = "DISTANCE: " + distance.ToString("0");
        }
    }
    public void StartWithMouse()
    {
        PlayerPrefs.SetString("ControlType","Mouse");
        LoadGameScene();
    }
    public void StartWithKeyboard()
    {
        PlayerPrefs.SetString("ControlType", "Keyboard"); 
        LoadGameScene(); 
    }
    public void Play()
    {
        panelInput.SetActive(true);
        controlsAnimator.SetBool("IsShow",true);
    }
    public void About()
    {
        panelAudio.SetActive(true);
    }
    public void Close()
    {
        controlsAnimator.SetBool("IsShow", false);
    }
    public void CloseVolumeOptions()
    {
        panelAudio.SetActive(false);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game"); 
    }
    public void PauseGame()
    {
        isGamePaused = true;
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isGamePaused=false;
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }
}
