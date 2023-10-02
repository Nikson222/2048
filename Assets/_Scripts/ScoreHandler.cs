using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText; 
    private int score = 0;

    public void Start()
    {
        UpdateText();
    }

    public void IncrementScore()
    {
        score++;
        UpdateText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        _scoreText.text = "Score: " + score.ToString();
    }
}
