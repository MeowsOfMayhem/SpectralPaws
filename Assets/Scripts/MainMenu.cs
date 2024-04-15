using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        Time.timeScale = 1.0f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

public void QuitGame()
    {

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
        Debug.Log("Quit button...");
    #endif
    }

    public void Credits()
    {
        Debug.Log("Credits button...");
    }
}


