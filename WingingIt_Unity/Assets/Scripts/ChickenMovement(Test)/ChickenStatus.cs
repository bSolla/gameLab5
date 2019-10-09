//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Kine and Paula, september 2019
//                       controls the stats of the chicken
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStatus : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public enum ChickenState { Normal, Hungry, Thirsty, Sad };
    public ChickenState currState = ChickenState.Normal;
    public int hunger = 100, thirst = 100, happiness = 100;
    private float timerHunger, timerThirst, timerHappiness;
    bool updateState = true;


    public StatusMenuUI menuUI;
    // public float realTime;
    // public DateTime currTime, lastTime;
    public string chickenName;
    bool isOpen;

    Chicken_Controller chickenController;
    FoodBowl food;
    WaterDispenser water;
    public FoodBowl Food { get => food; }
    public WaterDispenser Water {get => water; }
    // WaterDispenser water;
    PettingController petting;


    // public bool isHungry;


    // GameManager gm;

    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        // gm = FindObjectOfType<GameManager>();

        chickenController = GetComponent<Chicken_Controller>();

        timerHunger = 10;

        petting = GetComponent<PettingController>();

    }


    public void SearchReferences()
    {
        menuUI = GameObject.FindObjectOfType<StatusMenuUI>();

        if (GameManager.instance.CurrentSceneName == "Inside") //Actually it has to be in the inside, so we have to change this in the new scene
        {
            food = FindObjectOfType<FoodBowl>();
            water = FindObjectOfType<WaterDispenser>();
        }
        if (GameManager.instance.CurrentSceneName == "Outside") //Actually it has to be in the inside, so we have to change this in the new scene
        {
            food = FindObjectOfType<FoodBowl>();
        }

        updateState = true;
    }


    void Update()
    {

        switch (currState)
        {
            case ChickenState.Normal:
                UpdateNormalState();
                break;
            case ChickenState.Hungry:
                UpdateHungryState();
                break;
            case ChickenState.Thirsty:
                UpdateThirstyState();
                break;
            case ChickenState.Sad:
                UpdateSadState();
                break;
        }

        if (Input.GetMouseButtonUp(1))
        {
            print(currState);
            print(food);
        }

        // if(isOpen)
        // {

        // }

        // realTime = DateTime.Now;
        // currTime = DateTime.Now;
        // print(currTime.TimeOfDay);
        UpdateHunger();
        UpdateThirst();
        UpdateHappiness();
        if (menuUI == null)
        {
            menuUI = GameObject.Find("StatusMenu").GetComponent<StatusMenuUI>();        // Should be done in a cleaner way!

        }

        // if (Input.GetMouseButtonUp(0) && !menuUI.Panel.activeSelf && !chickenController.isLifted) 
        if (Input.GetMouseButtonUp(0) && !menuUI.Panel.activeSelf && !chickenController.isLifted) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider == this.gameObject.GetComponent<Collider>())
                {
                    menuUI.OpenMenu(this);
                }
            }           
        }
        if(happiness<0) happiness=0;
        if(hunger<0) hunger=0;
        if(thirst<0) thirst=0;
    }



    void UpdateNormalState()
    {
        // float precentChance = (hunger / 100f);
        // float rndValue = UnityEngine.Random.value; print (rndValue);
        // if(rndValue <= precentChance)
        // {
        //     print ("My chances are: " + precentChance + "%");
        //     currState = State.Hungry;
        // }
        if (updateState && hunger < 60)
        {
            currState = ChickenState.Hungry;
        }
        if (thirst < 50)
        {
            currState = ChickenState.Thirsty;
            // chickenController.movingPoint();  //---------If its outside maybe going inside to eat (check if its currently in the same scene as the player)--------
        }
        if (happiness < 50)
        {
            currState = ChickenState.Sad;
        }
    }

    void UpdateHungryState()
    {
        // if((GameManager.instance.currentSceneName == "Inside" && GameManager.instance.foodBoxAmount <= 0) || GameManager.instance.currentSceneName == "Outside" && GameManager.instance.foodVeggieAmount <= 0)
        if(food.avaliableFood <= 0)
        {
            // print ("Is dis bich workn");
            // chickenController.walkingToDoor = true;
            // currState = ChickenState.Normal;
            updateState = false;
        }
        else
        {
            // if (food.avaliableFood >= 0)
            // {
                chickenController.canMove = true;
                chickenController.GettingFood();
            // }

            if (hunger >= 100 || ((food.avaliableFood <= 0 && hunger >= 80)))
            {
                currState = ChickenState.Normal;
            }
        }
    }
    void UpdateThirstyState()
    {
        // if()
        if (water == null)
        {
            // chickenController.walkingToDoor = true;            
            Debug.Log("There is no water in this scene");
        }
        else
        {
            if (thirst >= 100 || (water.waterAvaliable <= 0 && thirst >= 50))
            {
                currState = ChickenState.Normal;
            }
            else
            {
                chickenController.canMove = true;             
                chickenController.GettingWater();

            }
        }

    }
    void UpdateSadState()
    {
        if (happiness > 50 || hunger<20 || thirst<20)
        {
            currState = ChickenState.Normal;
        }
    }

    void UpdateHunger()        //Constantly updating and checking if the hunger should go down
    {
        timerHunger -= Time.deltaTime;
        if (timerHunger <= 0 && hunger > 10)
        {
            timerHunger = 30;
            // timerHunger += 60 * 60;             // how long it should take before it drops, minute
            hunger -= 10;
        }
    }

    void UpdateThirst()    //Constantly updating and checking if the thirst should go down
    {
        timerThirst -= Time.deltaTime;
        if (timerThirst <= 0)
        {
            timerThirst = 30;
            thirst -= 5;
            // timerThirst -= 60 * 5;           // how long it should take before it drops, minute
        }
    }

    void UpdateHappiness()             //Constantly updating and checking if the happiness should go down
    {
        timerHappiness -= Time.deltaTime;
        if (timerHappiness <= 0)
        {
            timerHappiness = 120;
            happiness -= 10;

            // timerHappiness -= 60 * 15;           // how long it should take before it drops, minute
        }
    }
}
