using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score;
    private int _maxScore;

    public int Score => _score;
    public int MaxScore => _maxScore;

    public event Action ScoreChanged;

    public void Initialize()
    {
        _score = 0;
        ScoreChanged?.Invoke();
    }

    public void AddScore(int score)
    {
        _score += score;
        ScoreChanged?.Invoke();
    }

    public void SetMaxScore(int maxScore) => _maxScore = maxScore;

    public void Reset() => _score = 0;
}