//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                     A U T H O R  &  N O T E S
//                  coded by Teresa, September/October 2019
//             handles which chicken a nest is assigned to
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
        if (chickenToAssign)
        {
            if (!chickenToAssign.GetComponent<Chicken_Controller>().hasHome)
            {
                AssignHome();
            }
        }
        

    }

    public void AssignHome ()
    {
        //looks for an empty nest and assigns a homeless chicken to it
        Debug.Log ("assigning a home for a homeless chicken :3");

        foreach (GameObject nest in nests)
        {
            Nests tmp = nest.GetComponent<Nests>();
            if (!tmp.occupied)
            {
                chickenToAssign.GetComponent<Chicken_Controller>().hasHome = true;
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
