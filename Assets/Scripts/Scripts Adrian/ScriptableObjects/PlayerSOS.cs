using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "ScriptableObjects/PlayerScore")]

public class PlayerSOS : ScriptableObject
{
    public List<int> scores = new List<int>(); 
    public int score;

    private void OnEnable()
    {
        if (scores.Count == 0)
        {
            scores.AddRange(new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
        }
    }
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
            score = 0;
        }
    }
    public List<int> GetScores()
    {
        return new List<int>(scores); 
    }
    public bool IsInTopScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            if (score >= scores[i])
            {
                return true;
            }
        }
        return false;
    }
}
