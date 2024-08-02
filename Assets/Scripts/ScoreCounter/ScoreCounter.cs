using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;
    
    public int Score => _score;
    
    public event Action ScoreChanged;
    
    public void AddScore(int score)
    {
        _score += score;
        ScoreChanged?.Invoke();
    }
}