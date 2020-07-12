using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FlagType
{
    Level1,
    Level2,
    Level3,
    Level4,
}

public class Flag : MonoBehaviour
{
    public FlagType flag;
    public GameObject[] enemies;
    private Transform spawnPoint;
    private GameObject spawn;
    private void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPoint = spawn.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.IsEndLevel = true;

            switch (flag)
            {
                case FlagType.Level1:
                    SceneManager.LoadScene("Level1");
                    GameManager.instance.IsStartLevel = true;
                    GameManager.instance.SetlevelState(LevelState.level2);
                    break;
                case FlagType.Level2:
                    SceneManager.LoadScene("Level2");
                    GameManager.instance.IsStartLevel = true;
                    GameManager.instance.SetlevelState(LevelState.level3);

                    break;
                case FlagType.Level3:
                    SceneManager.LoadScene("Level3");
                    GameManager.instance.IsStartLevel = true;
                    GameManager.instance.SetlevelState(LevelState.level4);

                    break;
                case FlagType.Level4:
                    SceneManager.LoadScene("Level0");
                    GameManager.instance.IsStartLevel = true;
                    GameManager.instance.SetlevelState(LevelState.level1);

                    break;
            }
        }
    }
}
