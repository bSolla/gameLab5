using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{
    Chicken_Controller currentChick;
    bool chickLifted;
    public bool bushFull;

    private void Start()
    {
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
            else
            {
                chickLifted = false;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Chicken" && bushFull)
        {
            if (chickLifted && !currentChick.isLifted)
            {
                bushFull = false;
                FindObjectOfType<GameManager>().BerryMinigame = true;
                FindObjectOfType<GameManager>().ChickInBush = currentChick;
                GetComponent<ChangingScenes>().GoToScene("BerryPicking");
            }
        }        
    }
}
