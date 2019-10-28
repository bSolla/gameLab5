using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterMiniGameManager : MonoBehaviour
{
    public int score = 0;
    public int scoreGained = 1;
    private int finalScore;
    public Text scoreText, finalScoreText;
    public float gameTimer = 0;
    public Text timeLeft;
    public Button exitGameButton;
    public WaterContainerMovement waterer;
    public GameObject particleSystem;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
        exitGameButton.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft.text = Mathf.Round (gameTimer).ToString();
        gameTimer -= Time.deltaTime;
        scoreText.text = ("Score: " + score.ToString());

        if (gameTimer <= 0)
        {
            Debug.Log ("ending game");
            timeLeft.gameObject.SetActive (false);
            scoreText.gameObject.SetActive (false);
            finalScoreText.gameObject.SetActive(true);
            finalScore = score / 24;
            finalScoreText.text = ("Water gained: " + finalScore.ToString());
            exitGameButton.gameObject.SetActive(true);
            waterer.following = false;
            gameOver = true;
            particleSystem.gameObject.SetActive(false);
            GameManager.instance.WaterMinigame = true;
            GameManager.instance.WaterScore = finalScore;

        }
    }
}
