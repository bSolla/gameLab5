using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerryGameManager : MonoBehaviour
{
    [SerializeField] int numberOfPuzzlesPerLevel = 3;
    int currentLevel = 1;

    string levelsFolderPath = "BerryPicking/";

    ParticleSystem endOfPuzzleParticles;

    Text endOfGameText;
    [SerializeField] string endMessage = "You got food!!";

    void Start()
    {
        endOfPuzzleParticles = GetComponentInChildren<ParticleSystem>();
        endOfPuzzleParticles.playbackSpeed = 2f;

        endOfGameText = GetComponentInChildren<Text>();
        endOfGameText.text = "";

        StartMinigame();
    }


    IEnumerator ChangeCurrentPuzzle(GameObject currentPuzzle)
    {
        endOfPuzzleParticles.Play();
        
        yield return new WaitForSeconds(3.0f);

        Destroy(currentPuzzle);

        if (currentLevel <= 3)
            StartMinigame();
        else
        {
            yield return new WaitForSeconds(0.5f);
            endOfGameText.text = endMessage;
        }
    }

    void StartMinigame()
    {
        int puzzleNumber = Random.Range(1, numberOfPuzzlesPerLevel + 1);
        GameObject berryMinigame = Resources.Load(levelsFolderPath + currentLevel + "/" + puzzleNumber) as GameObject;

        Instantiate(berryMinigame, transform.position, transform.rotation);

        currentLevel++;
    }

    public void EndMiniGame(GameObject currentPuzzle)
    {
        StartCoroutine(ChangeCurrentPuzzle(currentPuzzle));
    }
}
