using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text healthText;
    public Text livesText;
    public playerController player;
    public Text scoreText;

    private int currentScore;
    private void Update()
    {
        scoreText.text = "Score : " + currentScore;
        livesText.text = "Lives : " + player.currentLives;
        healthText.text = "Health : " + player.currentHealth;
    }

    public void updateScore(int _score)
    {
        currentScore += _score;
    }
}
