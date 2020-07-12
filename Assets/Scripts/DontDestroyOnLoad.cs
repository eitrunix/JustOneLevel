using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] dontDestroy;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        GameObject[] UI = GameObject.FindGameObjectsWithTag("UI");

        if (objs.Length > 1)
            Destroy(this.gameObject);

        if (UI.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
