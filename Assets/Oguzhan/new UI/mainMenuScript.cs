using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class mainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void SoundOn()
    {
        AudioListener.volume = 1;
    }

    public void SoundOff()
    {
        AudioListener.volume = 0;
    }
}

