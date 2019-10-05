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

    public static GameManager instance = null;

    [SerializeField] GameObject chickenGroup;

    public string currentSceneName;
    public string CurrentSceneName { get => currentSceneName;}


    public List<GameObject> chickensList;

    // S T A T E   V A R I A B L E S

    bool bushIsFull=true;
    int foodAmount=50;
    int waterAmount;

    Chicken_Controller chickInBush;
    bool berryMinigame;
    public bool BerryMinigame { get => berryMinigame; set => berryMinigame = value; }
    public Chicken_Controller ChickInBush { get => chickInBush; set => chickInBush = value; }

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
     * Show the results of the minigammes (more food, bush empty...)            Everything done but water
     */



//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //When the game starts search the chickens in the first scene and add it to the list (this is only for testing the scenes we have now)
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += this.OnLoadCallback;

            chickensList = new List<GameObject>();
            GameObject[] array = GameObject.FindGameObjectsWithTag("Chicken");

            foreach (GameObject chick in array)
            {
                chickensList.Add(chick);
            }

            chickenGroup.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Each time a level is load the manager checks if we are in the coop or not and activate the chickens if they are suposed to be there
    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        currentSceneName = scene.name;
        if (CurrentSceneName == "Inside" || CurrentSceneName == "Outside")                 //Put the names of the scenes we are using here!!!!!!!
        {
            chickenGroup.SetActive(true);
        }
        else
        {
            chickenGroup.SetActive(false);
        }

        LoadStatsBetweenScenes();
        ActivateChickensToggle();
    }


    //This method check the current location of the chickens and call the method ActivateChicken in the ones which are in the current scene
    void ActivateChickensToggle()
    {
        foreach (GameObject chick in chickensList)
        {
            if (chick.GetComponent<Chicken_Controller>().CurrentLocation == CurrentSceneName)
            {
                chick.GetComponent<Chicken_Controller>().ActivateChicken();
            }
            else
            {
                chick.GetComponent<Chicken_Controller>().DeactivateChicken();
            }

            //chick.GetComponent<Chicken_Controller>().DoorPoint = GameObject.Find("DoorPoint").transform.position;
            chick.GetComponent<ChickenStatus>().SearchReferences();
        }
    }



    void LoadStatsBetweenScenes()
    {
        if (CurrentSceneName == "Inside")
        {
            FindObjectOfType<FoodBowl>().avaliableFood = foodAmount;
            FindObjectOfType<FoodBowl>().AddFood(0);
        }

        if (CurrentSceneName == "Outside")
        {
            if (berryMinigame)
            {
                berryMinigame = false;

                ChickInBush.GetComponent<ChickenStatus>().hunger += 30;         //Put the value we want to feed the chicken with the minigame
            }

            if (cutMinigame)
            {
                cutMinigame = false;
                
                foodAmount += (int)cuttingScore / 5;    
                FindObjectOfType<FoodBowl>().avaliableFood = foodAmount;
                FindObjectOfType<FoodBowl>().AddFood(0);               
            }

            FindObjectOfType<BerryBush>().bushFull = bushIsFull;
            
            //waterAmount
        }
    }


    public void SaveStatsBetweenScenes()   //Change this things to the scenes they actually are
    {
        if (CurrentSceneName=="Inside")
        {
            foodAmount = FindObjectOfType<FoodBowl>().avaliableFood;

        }

        if (CurrentSceneName=="Outside")
        {
            bushIsFull = FindObjectOfType<BerryBush>().bushFull;
            //waterAmount
        }
    }

}