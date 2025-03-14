using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //This will be a singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Keep it between scenes
        }
        else
        {
            Destroy(this.gameObject); //Destroy duplicates
        }
    }

    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
