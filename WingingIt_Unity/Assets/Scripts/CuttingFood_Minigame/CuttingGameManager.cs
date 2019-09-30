using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingGameManager : MonoBehaviour
{
    [SerializeField] float time = 30;
    [SerializeField] GameObject panel;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;

    float score = 0;
    bool gameRunning = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            }

        }
    }

    public void AddPoints(float points)
    {
        score += points;
        if (score<0)
        { score = 0;}
        scoreText.text = score.ToString();
    }
}
