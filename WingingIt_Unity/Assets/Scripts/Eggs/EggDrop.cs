// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      A U T H O R & N O T E S
//                                                coded by: Kine - September 2019
//                                  System that makes a chicken drop an egg after x-amount of time.
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


// using System.Collections;
// using System.Collections.Generic;
using System;
using UnityEngine;

public class EggDrop : MonoBehaviour
{   
// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public DateTime currentTime, oldTime;
    public GameObject eggPrefab;
    Vector3 dropTrans;
    public bool dropEgg;
    

// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      F U N C T I O N S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        print (System.DateTime.Now);
        oldTime = DateTime.Now;        
    }

    void Update()
    {
        // if(oldTime.Minute != currentTime.Minute)
        // {
        //     print ("Current: " + currentTime.Minute + " Old: " + oldTime.Minute);
        // }
        CheckNewTime();


        
    }

    // Checking the systems time to see if an egg should be dropped. If yes, DropAnEgg function get's called
    private void CheckNewTime()
    {
        // string currScene = GetComponent<GameManager>().currentSceneName;
        currentTime = DateTime.Now;
        if(GameObject.FindGameObjectWithTag("Egg") == null && (GameManager.instance.CurrentSceneName == "Outside" || GameManager.instance.CurrentSceneName == "Inside"))
        {
            if(oldTime.Date < currentTime.Date)
            {
                return;
            }
            else
            {
                if(oldTime.Hour < currentTime.Hour)
                {
                    DropAnEgg();
                }
                else
                {
                    if(currentTime.Minute - oldTime.Minute >= 0.9f || Input.GetKeyDown(KeyCode.E))
                    {
                        DropAnEgg();
                    }
                }
            }
        }

    }

    // Instantiates an egg, also tells the EggPickUp script that this is the new egg.
    private void DropAnEgg()
    {

        dropEgg = false;
        oldTime = currentTime;
        
        // dropTrans = GameObject.FindGameObjectWithTag("Chicken").transform.position;
        dropTrans = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.position;
        dropTrans = new Vector3 (dropTrans.x +2, 0.5f, dropTrans.z +2);
        
        GameObject newEgg = Instantiate (eggPrefab, dropTrans, transform.rotation);
        GetComponent<EggPickUp>().eggCol = newEgg.GetComponent<Collider>();

        
    }
    // private float whatEgg()
    // {

    // }
}
