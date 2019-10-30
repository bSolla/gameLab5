using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskForHelp : MonoBehaviour
{
    Chicken_Controller controller;
    ChickenUI chickenUI;
    public bool isLowStatus = false;
    void Start()
    {
        controller = GetComponent<Chicken_Controller>();
        chickenUI = controller.chickenUI;
        
    }

    void Update()
    {
        
    }
    public void AskingForHelp(int AskingForWhat)     //Hungry=0 - Thirsty=1  -  Sad=2
    {
        if(GameManager.instance.CurrentSceneName == controller.currentLocation)
        {
            if((GameManager.instance.foodBoxAmount<=0 && GameManager.instance.foodVeggieAmount <= 0))
            {
                
            //     if(!askForHelp.isLowStatus && food.currentAmount <= 0)
            //     {
            //         askForHelp.AskingForHelp(0);
            //         // print("Low Status");

            //     // chickenController.canMove = false;
            //     }
            // }
        
                print ("Rotation is correct");
                isLowStatus = true;

                chickenUI.AskForHelpUI(AskingForWhat);
                StartCoroutine(DelayAskForHelp());
            // }
            // else
            // {

                controller.LookAtPlayer();
            }

        }
        else
        {
            chickenUI.StopAskForHelpUI();
        }


    }
    public IEnumerator DelayAskForHelp()
    {
        if(isLowStatus)
        {   
            yield return new WaitForSeconds(10);
            // print ("Delay, first wait");
            
            controller.canMove=true;
            controller.target = transform.position;
            chickenUI.StopAskForHelpUI();
            // StartCoroutine(movingPoint(true));
            yield return new WaitForSeconds(10);
            // print ("Delay, second wait");

            isLowStatus=false;
        }
    }
}
