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
    LineRenderer followMouseLine;

    void Start()
    {
        berryArray = gameObject.GetComponentsInChildren<BerryBehavior>();
        totalBerryCount = berryArray.Length;

        trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
        trail.transform.parent = gameObject.transform;

        berryGameManager = GameObject.FindWithTag("Manager").GetComponent<BerryGameManager>();

        endOfPuzzleImage = GetComponentInChildren<EndImageLogic>();
        followMouseLine = GetComponentInChildren<LineRenderer>();

        followMouseLine.SetColors(Color.clear, Color.clear);
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

            followMouseLine.SetColors(Color.clear, Color.clear);
        }
    }

    void FixedUpdate()
    {
        if (mousePressed)
        {
            if (currentBerry < totalBerryCount && berryArray[currentBerry].beingHoveredByMouse)
            {
                trail.transform.position = berryArray[currentBerry].transform.position;

                followMouseLine.SetPosition(0, berryArray[currentBerry].transform.position);
                followMouseLine.SetColors(Color.white, Color.white);

                berryArray[currentBerry].ActivateFeedbackParticles();

                currentBerry++;

                if (currentBerry == totalBerryCount)
                {
                    followMouseLine.SetColors(Color.clear, Color.clear);
                    endOfPuzzleImage.StartFading();
                    berryGameManager.EndMiniGame(gameObject);
                }
            }
        }
    }
}
