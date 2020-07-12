using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    Pause,
    Title,
    StartLevel,
    EndLevel,
    Dead

}

public enum LevelState
{
    level1,
    level2,
    level3,
    level4,
    level5
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int currentScore;
    private int currentLives;
    private int currentHealth;
    private GameState state;
    private LevelState levelState;
    public playerController player;
    public UI ui;

    private bool isPlaying;
    private bool isPaused;
    private bool isOnTitle;
    private bool isStartLevel;
    private bool isEndLevel;

    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int CurrentLives { get => currentLives; set => currentLives = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool IsOnTitle { get => isOnTitle; set => isOnTitle = value; }
    public bool IsStartLevel { get => isStartLevel; set => isStartLevel = value; }
    public bool IsEndLevel { get => isEndLevel; set => isEndLevel = value; }
    public GameState State { get => state; set => state = value; }
    public LevelState LevelState { get => levelState; set => levelState = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        State = GameState.StartLevel;
        LevelState = LevelState.level1;
        CurrentHealth = 3;
        CurrentLives = 3;
        CurrentScore = 0;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isStartLevel = false;
            State = GameState.Play;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            State = GameState.Pause;
            if(Input.GetKeyDown(KeyCode.Escape) && IsPaused)
            {
                State = GameState.Play;
            }
        }
        switch (LevelState)
        {
            case LevelState.level1:
                break;
            case LevelState.level2:

                break;
            case LevelState.level3:

                playerController.instance.defaultAdditionalJumps = 1;
                break;
            case LevelState.level4:

                playerController.instance.defaultAdditionalJumps = 3;
                break;
            case LevelState.level5:

                playerController.instance.defaultAdditionalJumps = 5;
                break;
        }
        switch (State)
        {
            case GameState.Play:
                IsPlaying = true;
                IsPaused = false;
                break;
            case GameState.Pause:
                IsPaused = true;
                isPlaying = false;
                break;
            case GameState.Title:
                IsOnTitle = true;
                break;
            case GameState.StartLevel:
                IsStartLevel = true;
                IsPlaying = false;
                break;
            case GameState.EndLevel:
                IsEndLevel = true;
                IsPlaying = false;
                State = GameState.StartLevel;
                break;
            case GameState.Dead:
                ResetGame();
                break;
        }

        CheckHealth();
    }

    public void SetlevelState(LevelState _state)
    {
        LevelState = _state;
    }
    public void AddScore(int _score)
    {
        CurrentScore += _score;
    }
    public void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            State = GameState.Dead;
        }
    }

    void ResetGame()
    {
        Scene scn = SceneManager.GetActiveScene();
        State = GameState.StartLevel;

        CurrentHealth = 3;
        CurrentLives = 3;
        CurrentScore = 0;
        player.defaultAdditionalJumps = 0;
        SceneManager.LoadScene("Level0");
    }
}
