using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider music;
    public Slider sound;

    private void Start()
    {
        if (PlayerPrefs.HasKey("music")) music.value = PlayerPrefs.GetFloat("music");
        else music.value = 0.5f;
        if (PlayerPrefs.HasKey("sound")) sound.value = PlayerPrefs.GetFloat("sound");
        else sound.value = 0.45f;
    }

    public void startGame()
    {
        AudioManager.Instance.Play("start");
        Invoke("loadScene", 0.25f);
    }

    public void musicVolune()
    {
        PlayerPrefs.SetFloat("music", music.value);
        AudioManager.Instance.SetVolume(music.value);
    }

    public void soundVolume()
    {
        PlayerPrefs.SetFloat("sound", sound.value);
        AudioManager.Instance.SetAllVolume(sound.value);
    }

    public void quitGame()
    {
        AudioManager.Instance.Play("hit");
        Invoke("quit", 0.25f);
    }

    private void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void quit()
    {
        Application.Quit();
    }
}
