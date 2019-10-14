//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              controls when you are cutting and activates and desactivates the collider
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    bool isCutting = false;
    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D coll;

    [SerializeField] GameObject trialPref;
    GameObject currentTrial;

    Vector2 prevPos;
    Vector2 newPos;

    float minVel = 0.003f;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<CircleCollider2D>();
        cam = Camera.main;
    }


    //Calculates the position of the mouse and checks the input
    void Update()
    {
        newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPos;

        if (Input.GetMouseButtonDown(0)) 
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }

        prevPos = newPos;
    }


    //If the velocity while holding the button is enough it enables the collider
    private void UpdateCut()
    {
        float velocity = (newPos - prevPos).magnitude * Time.deltaTime;

        if(velocity>minVel)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }        
    }

    //Instantiate the trail and change the bool to true
    private void StartCutting()
    {
        isCutting = true;
        coll.enabled = false;

        currentTrial = Instantiate(trialPref, this.transform);        
    }

    //Destroys the trail and change the bool to false
    private void StopCutting()
    {
        isCutting = false;
        coll.enabled = false;

        currentTrial.transform.SetParent(null);
        Destroy(currentTrial,1.5f);
    }

    
}
