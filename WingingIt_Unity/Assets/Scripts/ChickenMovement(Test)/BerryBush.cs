using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{
    Chicken_Controller currentChick;
    public bool chickLifted;
    public bool bushFull;
    interactionConfirmation intCon;

    private void Start()
    {
        intCon = GetComponent<interactionConfirmation>();
        //We need to change the textures so there's no berrys in the bush
        //if (!bushFull)
        //{change to the non berry texture}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chicken")
        {
            currentChick = other.GetComponent<Chicken_Controller>();
            if (currentChick.isLifted)
            {
                chickLifted = true;
            }
            // else
            // {
            //     chickLifted = false;
            // }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Chicken" && bushFull)
        {
            if (chickLifted && !currentChick.isLifted)
            {
                bushFull = false; 

                // GetComponent<ChangingScenes>().GoToScene("BerryPicking");
            }
        }        
    }
}
