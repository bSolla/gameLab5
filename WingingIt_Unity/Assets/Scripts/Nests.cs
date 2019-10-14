//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                     A U T H O R  &  N O T E S
//                  coded by Teresa, September/October 2019
// just a way to assign a chicken to a specific nest and check if the nest is taken
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nests : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                         V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public bool occupied;
    public GameObject myChicken; 
    public GameObject chickenToSpawn;
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           M E T H O D S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void OnMouseDown()
    {
        if (!occupied)
        {
            newChicken();
        }
    }

    public void newChicken()
    {
        Instantiate (chickenToSpawn, new Vector3(0,2,0), Quaternion.identity);
    }
}