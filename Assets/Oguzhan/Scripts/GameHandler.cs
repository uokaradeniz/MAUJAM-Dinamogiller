using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private PlayerAttack playerAttack;
    private TextMeshProUGUI wonGameText;
    [HideInInspector] public TextMeshProUGUI overheatText;
    private TextMeshProUGUI lostGameText;
    private TextMeshProUGUI timerText;
    public float gameTimer = 188f;
    public float gameDuration;
    [HideInInspector] public TextMeshProUGUI overheatCDRText;
    private RectTransform returnToMenu;

    public bool wonGame;
    public bool lostGame;

    // Start is called before the first frame update
    void Start()
    {
        overheatCDRText = GameObject.Find("OverheatCDR").GetComponent<TextMeshProUGUI>();
        overheatText = GameObject.Find("OverheatText").GetComponent<TextMeshProUGUI>();
        wonGameText = GameObject.Find("WonGameText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        lostGameText = GameObject.Find("LostGameText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        gameTimer = gameDuration;
        returnToMenu = GameObject.Find("ReturnToMenu").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lostGame)
            timerText.text = " : " + Mathf.Round(gameTimer).ToString();
        if (lostGame || wonGame)
            returnToMenu.localScale = new Vector3(1, 1, 1);

        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
            lostGame = true;

        if (playerAttack.score >= 100)
        {
            wonGame = true;
            wonGameText.text = "YOU WIN";
        }

        scoreText.text = " : " + playerAttack.score.ToString();

        if (lostGame)
            lostGameText.text = "YOU LOST";
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}