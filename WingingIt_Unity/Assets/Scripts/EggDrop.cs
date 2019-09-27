// using System.Collections;
// using System.Collections.Generic;
using System;
using UnityEngine;

public class EggDrop : MonoBehaviour
{   
    public DateTime currentTime, oldTime;
    public GameObject commonEggPrefab, rareEggPrefab, legendaryEggPrefab;
    public bool dropEgg;
    

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
    private void CheckNewTime()
    {
        currentTime = DateTime.Now;

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
                if(currentTime.Minute - oldTime.Minute >= 5f)
                {
                    DropAnEgg();
                }
            }
        }

    }
    private void DropAnEgg()
    {
        dropEgg = false;
        oldTime = currentTime;

        float whatEgg = UnityEngine.Random.value;
        if(whatEgg >= 0.4f)      //Common - 60% Drop rate
        {
            print ("I dropped a common egg");
            GameObject newEgg = Instantiate (commonEggPrefab, transform.position, transform.rotation);
            newEgg.GetComponent<WhatEgg>().whatEgg = 1;
        }
        if(whatEgg > 0.1f && whatEgg < 0.4f)        // Rare - 30% Drop rate
        {
            print ("I dropped a rare egg");
            GameObject newEgg = Instantiate (rareEggPrefab, transform.position, transform.rotation);
            newEgg.GetComponent<WhatEgg>().whatEgg = 2;



        }
        if(whatEgg <= 0.1f)             //Legendary - 10% drop rate
        {
            print ("I dropped a legendary egg");
            GameObject newEgg = Instantiate (legendaryEggPrefab, transform.position, transform.rotation);
            newEgg.GetComponent<WhatEgg>().whatEgg = 3;


        }
    }
    // private float whatEgg()
    // {

    // }
}
