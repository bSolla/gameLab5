// using System.Collections;
// using System.Collections.Generic;
using System;
using UnityEngine;

public class EggDrop : MonoBehaviour
{   
    public DateTime currentTime, oldTime;
    public GameObject eggPrefab;
    Vector3 dropTrans;
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
        if(GameObject.FindGameObjectWithTag("Egg") == null)
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
    private void DropAnEgg()
    {

        dropEgg = false;
        oldTime = currentTime;
        
        dropTrans = GameObject.FindGameObjectWithTag("Chicken").transform.position;
        dropTrans = new Vector3 (dropTrans.x +2, 0.5f, dropTrans.z +2);
        
        GameObject newEgg = Instantiate (eggPrefab, dropTrans, transform.rotation);

        
    }
    // private float whatEgg()
    // {

    // }
}
