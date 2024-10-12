using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "ScriptableObjects/PlayerScore")]

public class PlayerSOS : ScriptableObject
{
    public List<int> scores = new List<int>(); 
    public int score; 

    public void AddScore(int newScore)
    {
        scores.Add(newScore); 
        if (scores.Count > 10) 
        {
            scores.RemoveAt(0); 
        }
    }
    public void ResetScore()
    {
        if (score > 0) 
        {
            scores.Add(score); 
        }
        score = 0; 
    }
    public List<int> GetScores()
    {
        return new List<int>(scores); 
    }
    public bool IsInTopScores()
    {
        scores.Sort();
        scores.Reverse();
        return scores.Contains(score);
    }
}
