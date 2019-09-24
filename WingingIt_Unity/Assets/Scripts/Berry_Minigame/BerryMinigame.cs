using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryMinigame : MonoBehaviour
{
    private bool mousePressed = false;
    private int currentBerry = 0;
    private int totalBerryCount = 0;

    BerryBehavior[] berryArray;
    [SerializeField] GameObject trailPrefab;
    GameObject trail;

    BerryGameManager berryGameManager;

    EndImageLogic endOfPuzzleImage;

    void Start()
    {
        berryArray = gameObject.GetComponentsInChildren<BerryBehavior>();
        totalBerryCount = berryArray.Length;

        trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
        trail.transform.parent = gameObject.transform;

        berryGameManager = GameObject.FindWithTag("Manager").GetComponent<BerryGameManager>();

        endOfPuzzleImage = GetComponentInChildren<EndImageLogic>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }
        else if (Input.GetMouseButtonUp(0) && currentBerry < totalBerryCount)
        {
            mousePressed = false;
            currentBerry = 0;

            Destroy(trail);
            trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
            trail.transform.parent = gameObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (mousePressed)
        {
            if (currentBerry < totalBerryCount && berryArray[currentBerry].beingHoveredByMouse)
            {
                trail.transform.position = berryArray[currentBerry].transform.position;
                berryArray[currentBerry].ActivateFeedbackParticles();

                currentBerry++;

                if (currentBerry == totalBerryCount)
                {
                    endOfPuzzleImage.StartFading();
                    berryGameManager.EndMiniGame(gameObject);
                }
            }
        }
    }
}
