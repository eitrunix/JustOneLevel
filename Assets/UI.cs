using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text healthText;
    public Text livesText;
    public Text scoreText;
    public Text startToPlay;
    public Text paused;
    public GameObject bgPause;

    private void Update()
    {
        if(!GameManager.instance.IsStartLevel)
        {
            startToPlay.enabled = false;
        }
        else
        {
            startToPlay.enabled = true;
        }

        if(GameManager.instance.IsPaused)
        {
            paused.enabled = true;
            bgPause.SetActive(true);
        }
        else
        {
            paused.enabled = false;
            bgPause.SetActive(false);

        }
    }
    private void LateUpdate()
    {
        scoreText.text = "Score : " + GameManager.instance.CurrentScore;
        livesText.text = "Lives : " + GameManager.instance.CurrentLives;
        healthText.text = "Health : " + GameManager.instance.CurrentHealth;
    }


}
