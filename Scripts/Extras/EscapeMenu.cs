using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject health;

    public static bool isPaused = false;  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                showPause();
            else
                hidePause();
        }
    }

    private void showPause()
    {
        isPaused = true;
        Time.timeScale = 0;
        menu.SetActive(true);
        health.SetActive(false);
    }

    public void hidePause()
    {
        isPaused = false;
        Time.timeScale = 1;
        menu.SetActive(false);
        health.SetActive(true);
    }

    public void mainMenu()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        AudioManager.Instance.Play("hit");
        Invoke("quit", 0.25f);
    }

    private void quit()
    {
        Application.Quit();
    }
}
