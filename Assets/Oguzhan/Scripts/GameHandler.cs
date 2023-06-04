using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private PlayerAttack playerAttack;
    private TextMeshProUGUI wonGameText;

    public bool wonGame;
    // Start is called before the first frame update
    void Start()
    {
        wonGameText = GameObject.Find("WonGameText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.score >= 100)
        {
            wonGame = true;
            wonGameText.text = "YOU WIN";
        }

        scoreText.text = "Score: " + playerAttack.score.ToString();
    }
}
