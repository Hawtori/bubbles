using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TMP_Text highscore;
    public TMP_Text score;

    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("Highscore"))
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        else highscore.text = "Highscore not found!";
        if (PlayerPrefs.HasKey("Score"))
            score.text = "" + PlayerPrefs.GetInt("Score");
        else score.text = "Score not found!";
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
