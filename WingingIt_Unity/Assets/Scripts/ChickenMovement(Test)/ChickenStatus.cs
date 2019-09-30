using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStatus : MonoBehaviour
{
    public enum State { Normal, Hungry, Thirsty, Sad };
    public State currState = State.Normal;
    public int hunger = 100, thirst = 100, happiness = 100;
    private float tHunger, tThirst, tHappiness;
    StatusMenuUI menuUI;
    // public float realTime;
    // public DateTime currTime, lastTime;
    public string chickenName;
    bool isOpen;

    Chicken_Controller chickenController;
    FoodBowl food;
    public FoodBowl Food { get => food; }
    WaterDispenser water;

    // public bool isHungry;


    GameManager gm;



    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        chickenController = GetComponent<Chicken_Controller>();

        tHunger = 10;
    }

    public void SearchReferences()
    {
        menuUI = GameObject.FindObjectOfType<StatusMenuUI>();

        if (gm == null) //--------------It should be another way to fix this---------- This is here because this method is called before the start method, so there's not gm reference yet
        {
            gm = FindObjectOfType<GameManager>();
        }
        if (gm.CurrentSceneName == "Outside") //Actually it has to be in the inside, so we have to change this in the new scene
        {
            food = FindObjectOfType<FoodBowl>();
        }
    }

    void Update()
    {

        switch (currState)
        {
            case State.Normal: UpdateNormalState(); break;
            case State.Hungry: UpdateHungryState(); break;
            case State.Thirsty: UpdateThirstyState(); break;
            case State.Sad: UpdateSadState(); break;
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
        if (hunger < 60)
        {
            currState = State.Hungry;
        }
        if (thirst < 50)
        {
            currState = State.Thirsty;
            // chickenController.movingPoint();  //---------If its outside maybe going inside to eat (check if its currently in the same scene as the player)--------
        }
        if (happiness < 50)
        {
            currState = State.Sad;
        }
    }

    void UpdateHungryState()
    {
        // if(food.avaliableFood > 0)
        // {
        // float precentChance = (food / 100f);
        // if(Random.value <= precentChance)
        // {

        chickenController.GettingFood();



        // }


        // chickenController.GettingFood();
        // }
        // else
        // {
        if (hunger >= 50)
        {
            currState = State.Normal;
        }
        // }
    }
    void UpdateThirstyState()
    {
        // if()

    }
    void UpdateSadState()
    {
        if (hunger > 50 && thirst > 50)
        {
            currState = State.Normal;
        }
    }

    void UpdateHunger()        //Constantly updating and checking if the hunger should go down
    {
        tHunger -= Time.deltaTime;
        if (tHunger <= 0 && hunger > 10)
        {
            tHunger = 10;
            // tHunger += 60 * 60;             // how long it should take before it drops, minute
            hunger -= 10;
        }
    }

    void UpdateThirst()    //Constantly updating and checking if the thirst should go down
    {
        tThirst -= Time.deltaTime;
        if (tThirst <= 0)
        {
            tThirst = 10;
            thirst -= 5;
            // tThirst -= 60 * 5;           // how long it should take before it drops, minute
        }
    }

    void UpdateHappiness()             //Constantly updating and checking if the happiness should go down
    {
        tHappiness -= Time.deltaTime;
        if (tHappiness <= 0)
        {
            tHappiness = 10;
            happiness -= 10;

            // tHappiness -= 60 * 15;           // how long it should take before it drops, minute
        }
    }
}
