//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              controls in which scene we are and if the chickens should be there
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    [SerializeField] GameObject chickenGroup;

    string currentSceneName;
    public string CurrentSceneName { get => currentSceneName;}


    List<GameObject> chickensList;

    // S T A T E   V A R I A B L E S

    bool bushIsFull;
    int foodAmount;
    int waterAmount;

    bool cutMinigame=false;
    float cuttingScore;
    public bool CutMinigame { get => cutMinigame; set => cutMinigame = value; }
    public float CuttingScore { get => cuttingScore; set => cuttingScore = value; }


    /*To do:
     * 
     * Save the amount of food and water each time we change scenes 
     * 
     * Save the state of the bush each time we change scenes
     * 
     * Make them be the same when we come back to the scene
     * 
     * Show the results of the minigammes (more food, bush empty...)
     */



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //When the game starts search the chickens in the first scene and add it to the list (this is only for testing the scenes we have now)
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        chickensList = new List<GameObject>();
        GameObject[] array = GameObject.FindGameObjectsWithTag("Chicken");

        foreach (GameObject chick in array)
        {
            chickensList.Add(chick);
        }

        chickenGroup.SetActive(false);
    }


    //Each time a level is load the manager checks if we are in the coop or not and activate the chickens if they are suposed to be there
    private void OnLevelWasLoaded(int level)
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if (CurrentSceneName == "Inside"|| CurrentSceneName == "Outside")                 //Put the names of the scenes we are using here!!!!!!!
        {
            chickenGroup.SetActive(true);
            ActivateChickens();
        }
        else
        {
            chickenGroup.SetActive(false);
        }    
    }

    //This method check the current location of the chickens and call the method ActivateChicken in the ones which are in the current scene
    void ActivateChickens()
    {
        foreach (GameObject chick in chickensList)
        {
            if (chick.GetComponent<Chicken_Controller>().CurrentLocation == CurrentSceneName)
            {
                chick.GetComponent<Chicken_Controller>().ActivateChicken();
            }
            else
            {
                chick.GetComponent<Chicken_Controller>().DesactivateChicken();
            }

            chick.GetComponent<Chicken_Controller>().DoorPoint = GameObject.Find("DoorPoint").transform.position;
            chick.GetComponent<ChickenStatus>().SearchReferences();
        }
    }



    void LoadStatsBetweenScenes()
    {
        if (CurrentSceneName == "Inside")
        {
            FindObjectOfType<FoodBowl>().avaliableFood = foodAmount;
            FindObjectOfType<WaterDispenser>().waterAvaliable = waterAmount;

        }

        if (CurrentSceneName == "Outside")
        {
            if (cutMinigame)
            {
                cutMinigame = false;
                //foodAmount += cuttingScore / 4;                           CHANGE FLOAT TO INT SO WE CAN ADD IT

            }

            FindObjectOfType<BerryBush>().bushFull = bushIsFull;
            //waterAmount

            //If you come back from minigame add the food
            
        }
    }


    public void SaveStatsBetweenScenes()   //Change this things to the scenes they actually are
    {
        if (CurrentSceneName=="Inside")
        {
            foodAmount = FindObjectOfType<FoodBowl>().avaliableFood;
            waterAmount = FindObjectOfType<WaterDispenser>().waterAvaliable;

        }

        if (CurrentSceneName=="Outside")
        {
            bushIsFull = FindObjectOfType<BerryBush>().bushFull;
            //waterAmount
        }
    }

}
