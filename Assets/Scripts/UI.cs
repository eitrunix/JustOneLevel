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
    public Text inst1;
    public Text inst2;
    public Text inst3;
    public Text inst4;
    private void Update()
    {

        if (GameManager.instance.IsPaused)
        {
            paused.enabled = true;
            bgPause.SetActive(true);
        }
        else
        {
            paused.enabled = false;
            bgPause.SetActive(false);

        }
        if (!GameManager.instance.IsStartLevel)
        {
            startToPlay.enabled = false;
            inst1.enabled = false;
            inst2.enabled = false;
            inst3.enabled = false;
            inst4.enabled = false;

        }
        else
        {
            startToPlay.enabled = true;


            if (GameManager.instance.LevelState == LevelState.level1)
            {
                inst1.enabled = true;
            }
            else
            {
                inst1.enabled = false;
            }
            if (GameManager.instance.LevelState == LevelState.level2)
            {
                inst2.enabled = true;
            }
            else
            {
                inst2.enabled = false;

            }
            if (GameManager.instance.LevelState == LevelState.level3)
            {
                inst3.enabled = true;
            }
            else
            {
                inst3.enabled = false;

            }
            if (GameManager.instance.LevelState == LevelState.level4)
            {
                inst4.enabled = true;
            }
            else
            {
                inst4.enabled = false;

            }
        }


    }

    private void LateUpdate()
    {
        scoreText.text = "Score : " + GameManager.instance.CurrentScore;
        livesText.text = "Lives : " + GameManager.instance.CurrentLives;
        healthText.text = "Health : " + GameManager.instance.CurrentHealth;
    }


}
