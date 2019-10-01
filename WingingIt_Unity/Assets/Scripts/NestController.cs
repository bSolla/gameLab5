//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                     A U T H O R  &  N O T E S
//                  coded by Teresa, September 2019
//             handles which chicken the nest is assigned to
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NestController : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                         V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
public GameObject chickenToAssign;
public GameObject[] nests;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           M E T H O D S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    void Start()
    {
        
    }

    void Update()
    {
        chickenToAssign = GameObject.FindGameObjectWithTag("Chicken");
        if (!chickenToAssign.GetComponent<ChickenController>().hasHome)
        {
            AssignHome();
        }

        if (chickenToAssign = null)
        {
            Debug.Log ("no homeless chickens! yay!");
        }
    }

    public void AssignHome ()
    {
        Debug.Log ("assigning a home for a homeless chicken :3");

        foreach (GameObject nest in nests)
        {
            Nests tmp = nest.GetComponent<Nests>();
            if (!tmp.occupied)
            {
                chickenToAssign.GetComponent<ChickenController>().hasHome = true;
                tmp.myChicken = chickenToAssign;
                chickenToAssign.gameObject.tag = "AssignedChicken";
                chickenToAssign = null;
                tmp.occupied = true;
                //AssignHome();
                return;
                
            }
        }
    }
}
