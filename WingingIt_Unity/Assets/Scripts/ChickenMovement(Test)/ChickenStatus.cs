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

    public enum State { Normal, Hungry, Thirsty, Sad };
    public State currState = State.Normal;
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
    public FoodBowl Food { get => food; }
    WaterDispenser water;
    PettingController petting;


    // public bool isHungry;


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        chickenController = GetComponent<Chicken_Controller>();

        timerHunger = 10;

        petting = GetComponent<PettingController>();

    }


    public void SearchReferences()
    {
        menuUI = GameObject.FindObjectOfType<StatusMenuUI>();
        
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
            case State.Normal:
                UpdateNormalState();
                break;
            case State.Hungry:
                UpdateHungryState();
                break;
            case State.Thirsty:
                UpdateThirstyState();
                break;
            case State.Sad:
                UpdateSadState();
                break;
        }

        if (Input.GetMouseButtonUp(1))
        {
            print(currState);
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
            currState = State.Hungry;
        }
        //if (thirst < 50)
        //{
        //    currState = State.Thirsty;
        //    // chickenController.movingPoint();  //---------If its outside maybe going inside to eat (check if its currently in the same scene as the player)--------
        //}
        if (happiness < 50)
        {
            currState = State.Sad;
        }
    }

    void UpdateHungryState()
    {
        if (food == null)
        {
            chickenController.walkingToDoor = true;
            currState = State.Normal;
            updateState = false;
        }
        else
        {
            if (food.avaliableFood > 0)
            {
                chickenController.GettingFood();
            }

            if (hunger >= 100 || (food.avaliableFood <= 0 && hunger >= 50))
            {
                currState = State.Normal;
            }
        }
        // if(food.avaliableFood > 0)
        // {
        // float precentChance = (food / 100f);
        // if(Random.value <= precentChance)
        // {

        // chickenController.GettingFood();



        // }


        // chickenController.GettingFood();
        // }
        // else
        // {
        //if (hunger >= 100 || (food.avaliableFood <= 0 && hunger >= 50))
        //{
        //    currState = State.Normal;
        //}
        // }
    }


    void UpdateThirstyState()
    {
        
        if (water == null)
        {
            Debug.Log("There is no water in this scene");
        }
        else
        {
            if (thirst >= 100 || (water.waterAvaliable <= 0 && thirst >= 50))
            {
                currState = State.Normal;
            }
        }

    }


    void UpdateSadState()
    {
        if (happiness > 50 || hunger<20 || thirst<20)
        {
            currState = State.Normal;
        }
    }


    void UpdateHunger()        //Constantly updating and checking if the hunger should go down
    {
        timerHunger -= Time.deltaTime;
        if (timerHunger <= 0 && hunger > 10)
        {
            timerHunger = 10;
            // tHunger += 60 * 60;             // how long it should take before it drops, minute
            hunger -= 10;
        }
    }

    void UpdateThirst()    //Constantly updating and checking if the thirst should go down
    {
        timerThirst -= Time.deltaTime;
        if (timerThirst <= 0)
        {
            timerThirst = 10;
            thirst -= 5;
            // tThirst -= 60 * 5;           // how long it should take before it drops, minute
        }
    }

    void UpdateHappiness()             //Constantly updating and checking if the happiness should go down
    {
        timerHappiness -= Time.deltaTime;
        if (timerHappiness <= 0)
        {
            timerHappiness = 60;
            happiness -= 10;

            // tHappiness -= 60 * 15;           // how long it should take before it drops, minute
        }
    }
}
