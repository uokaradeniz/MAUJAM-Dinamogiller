using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLineNextSceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("BerkScene 5", LoadSceneMode.Single);
    }
}
