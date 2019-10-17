//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Len, september 2019
//  manages the puzzle itself, delegating individual berry checks to BerryBehavior but 
//  controling trails/lines and puzzle end behaviors
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BerryMinigame : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private bool mousePressed = false;
    private int currentBerry = 0;
    private int totalBerryCount = 0;

    BerryBehavior[] berryArray;
    [SerializeField] GameObject trailPrefab;
    GameObject trail;

    BerryGameManager berryGameManager;

    EndImageLogic endOfPuzzleImage;
    LineRenderer followMouseLine;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // caching of variables and initial settings
    void Start()
    {
        berryArray = gameObject.GetComponentsInChildren<BerryBehavior>();
        totalBerryCount = berryArray.Length;

        trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
        trail.transform.parent = gameObject.transform;
        
        berryGameManager = GetComponentInParent<BerryGameManager>();

        endOfPuzzleImage = GetComponentInChildren<EndImageLogic>();
        followMouseLine = GetComponentInChildren<LineRenderer>();

        followMouseLine.SetColors(Color.clear, Color.clear);
    }


    // deals with the mouse input: if it's down, it sets the mousePressed bool to true
    // if its up and you've not reached the last berry, it restarts the trail and berry count
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
            if (currentBerry != 0)
                followMouseLine.SetColors(Color.white, Color.white);
        }
        else if (Input.GetMouseButtonUp(0) && currentBerry < totalBerryCount)
        {
            mousePressed = false;

            berryGameManager.LosePoints();
            followMouseLine.SetColors(Color.clear, Color.clear);
        }
    }


    // uses the mousePressed bool: if it is pressed, checks "do we have berries left? is the proper berry
    // being hovered by the mouse?" Passing the check updates the rainbow trail and the white trail, and
    // adds to the current berry count.
    // if it's the last berry, it makes the white trail invisible and starts the end of game feedback
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
