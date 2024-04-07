using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;

public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button...");
    }

    public void Credits()
    {
        Debug.Log("Credits button...");
    }
}


