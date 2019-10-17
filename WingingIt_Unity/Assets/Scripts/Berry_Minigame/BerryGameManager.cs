//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Len, september 2019
//  controls which puzzle is instantiated, and keeps track of current level of difficulty
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BerryGameManager : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [SerializeField] int numberOfPuzzlesPerLevel = 3;
    int currentLevel = 1;

    string levelsFolderPath = "BerryPicking/";

    ParticleSystem endOfPuzzleParticles;

    Text endOfGameText;
    [SerializeField] string endMessage = "Food amount: ";

    Text points;
    int currentPoints = 0;
    int cumulativePoints = 0;


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // caches variables and sets initial values, then starts first puzzle
    void Start()
    {
        endOfPuzzleParticles = GetComponentInChildren<ParticleSystem>();
        endOfPuzzleParticles.playbackSpeed = 2f;

        Text[] textArray = GetComponentsInChildren<Text>();
        endOfGameText = textArray[0];
        endOfGameText.text = "";
        points = textArray[1];

        StartMinigame();
    }


    // coroutine that mamages the puzzle change: plays the end of puzzle particles, waits 
    // and checks if it's the last level of difficulty or not. If not, loads the next level
    // if it is the last, displays "you got food!"
    IEnumerator ChangeCurrentPuzzle(GameObject currentPuzzle)
    {
        endOfPuzzleParticles.Play();

        cumulativePoints += currentPoints;

        yield return new WaitForSeconds(3.0f);

        Destroy(currentPuzzle);

        if (currentLevel <= 3)
            StartMinigame();
        else
        {
            yield return new WaitForSeconds(0.5f);

            endOfGameText.text = endMessage + cumulativePoints.ToString();
            yield return new WaitForSeconds(3f);

            GameManager.instance.ChickInBush.GetComponent<ChickenStatus>().hunger += cumulativePoints;

            GetComponent<ChangingScenes>().GoToScene("Outside");      //-----------------Go back to the coop, just for now, has to be changed
        }
    }


    // deals with puzzle selection automatically. Picks a random number between 1 and numberOfPuzzlesPerLevel,
    // loads it from the resources folder and instantiates it, increasing the currentLevel counter
    void StartMinigame()
    {
        int puzzleNumber = Random.Range(1, numberOfPuzzlesPerLevel + 1);
        GameObject berryMinigame = Resources.Load(levelsFolderPath + currentLevel + "/" + puzzleNumber) as GameObject;

        Instantiate(berryMinigame, transform.position, transform.rotation, gameObject.transform);

        currentPoints = 10 * currentLevel + 5;
        points.text = currentPoints.ToString();

        currentLevel++;
    }


    // public setter method, used from outside this class. Starts the ChangeCurrentPuzzle coroutine
    public void EndMiniGame(GameObject currentPuzzle)
    {
        StartCoroutine(ChangeCurrentPuzzle(currentPuzzle));
    }


    public void LosePoints()
    {
        if (currentPoints > 5)
        {
            currentPoints--;
            points.text = currentPoints.ToString();
        }
    }
}
