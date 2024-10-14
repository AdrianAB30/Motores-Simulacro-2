using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverController : MonoBehaviour
{
    public Button btnPlay;
    public TMP_Text scoreText;
    public TMP_Text bestScoreMessage;
    public TMP_Text topScoresText;
    public PlayerSOS playerScore;

    void Start()
    {
        btnPlay.onClick.AddListener(() => ReturnMenu());
        ShowScore();
        DisplayTopScores();
        scoreText.gameObject.SetActive(true);
    }
    private void ShowScore()
    {
        scoreText.text = "Tu Puntaje: " + playerScore.score;
        bestScoreMessage.gameObject.SetActive(true);

        if (playerScore.IsInTopScores())
        {
            bestScoreMessage.text = "Felicitaciones, estas en el top 10";
        }
        else
        {
            bestScoreMessage.text = "Intentalo nuevamente, no estas en el top 10";
        }
    }
    private void DisplayTopScores()
    {
        List<int> topScores = playerScore.GetScores();
        topScoresText.text = ""; 

        for (int i = 0; i < topScores.Count; i++)
        {
            topScoresText.text += "Top " + (i + 1) + ": " + topScores[i] + "\n";
        }
    }

    void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
