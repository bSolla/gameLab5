//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              controls the time and the score of the game
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingGameManager : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    [SerializeField] float time = 30;
    [SerializeField] GameObject panel;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;

    float score = 0;
    bool gameRunning = true;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Controls the timer, when the time is over ends the game
    void Update()
    {
        if (gameRunning)
        {
            time -= Time.deltaTime;

            timeText.text = time.ToString("0");

            if (time <= 0)
            {
                Spawn[] spawn = FindObjectsOfType<Spawn>();
                Destroy(spawn[0]);
                Destroy(spawn[1]);
                Destroy(FindObjectOfType<Knife>());

                panel.SetActive(true);
                gameRunning = false;

                GameManager.instance.CutMinigame = true;
                GameManager.instance.CuttingScore = score;
            }
        }
    }


    //This method is called from other script to change the score each time something is cut
    public void AddPoints(float points)
    {
        score += points;
        if (score<0)
        { score = 0;}
        scoreText.text = score.ToString();
    }
}
