using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PelletDropManager : MonoBehaviour
{

    public int score = 0;
    public int scoreGained = 1;
    public Text scoreText;
    public float gameTimer = 0;
    public Text timeLeft;
    public Button exitGameButton;
    public FeederMovement feeder;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        exitGameButton.gameObject.SetActive(false);
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
            exitGameButton.gameObject.SetActive(true);
            feeder.following = false;

        }
    }

    void StartGame ()
    {
        // do we need this tho idk
    }
}
