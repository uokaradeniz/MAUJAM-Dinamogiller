using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour

{

    public Button startButton;
    public Button stopButton;
    public Button settingButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;


        startButton = root.Q<Button>("ButtonStart");
        stopButton = root.Q<Button>("ButtonStop");
        settingButton = root.Q<Button>("ButtonSettings");

        startButton.clicked += StartButtonPressed;
        stopButton.clicked += exitButtonPressed;



    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void StartButtonPressed()
    {
        SceneManager.LoadScene("OguzTest");
        Debug.Log("start pressed");
    }

    void exitButtonPressed()
    {
        Application.Quit();
        Debug.Log("game closed");
    }
}
