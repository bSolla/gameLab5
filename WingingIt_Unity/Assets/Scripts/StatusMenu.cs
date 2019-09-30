// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      A U T H O R & N O T E S
//                                                coded by: Kine - September 2019
//                              Placeholder script for food. Also stores the amount of food avaliable in the bowl.
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusMenu : MonoBehaviour
{
// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public enum State {Normal, Hungry, Thirsty, Sad};
    public State currState = State.Normal;
    public int hunger = 100, thirst = 100, happiness = 100;
    private float tHunger, tThirst, tHappiness;
    public GameObject menuUI;
    // public float realTime;
    // public DateTime currTime, lastTime;
    public Text chickenNameUi;
    public String chickenName;
    public Slider sliderHunger, sliderThirst, sliderHappiness;
    bool isOpen;

    ChickenController chickenController;
    FoodBowl food;
    WaterDispenser water;
    PettingController petting;


    // public bool isHungry;

// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      F U N C T I O N S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        chickenController = GetComponent<ChickenController>();
        food = chickenController.foodBowl.GetComponent<FoodBowl>();
        water = chickenController.waterBowl.GetComponent<WaterDispenser>();
        petting = GetComponent<PettingController>();


        CloseMenu();

        tHunger = 10;
    }

    void Update()
    {

        switch(currState)
        {
            case State.Normal: UpdateNormalState(); break;
            case State.Hungry: UpdateHungryState(); break;
            case State.Thirsty: UpdateThirstyState(); break;
            case State.Sad: UpdateSadState(); break;
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
        if(isOpen)
        {
            if(Input.GetMouseButtonUp(0))
            {
                CloseMenu();
            }
              
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Collider col = this.gameObject.GetComponent<Collider>();
            if(Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider == col && Input.GetMouseButtonUp(0))
                {
                    OpenMenu();
                }
            }
        }
    }
    void UpdateNormalState()
    {
        if(hunger < 25)
        {
            currState = State.Hungry;
        }
        if(thirst < 25)
        {
            currState = State.Thirsty;
        }
        if(happiness < 30 && (hunger > 20 && thirst > 20))
        {
            currState = State.Sad;
        }
    }
    void UpdateHungryState()
    {
        chickenController.GettingFood();

        if(hunger >= 100 || (food.avaliableFood <= 0 && hunger >= 50))
        {
            currState = State.Normal;
        }
    }
    void UpdateThirstyState()
    {
        chickenController.GettingWater();
        if(thirst >= 100 || (water.waterAvaliable <= 0 && thirst >= 50))
        {
            currState = State.Normal;
        }

    }
    void UpdateSadState()
    {
        if (happiness > 50 || hunger<20 || thirst<20)
        {
            currState = State.Normal;
        }
    }

    void OpenMenu()
    {
        menuUI.SetActive(true);
        isOpen = true;
        sliderHunger.value = hunger;
        sliderThirst.value = thirst;
        sliderHappiness.value = happiness;
        chickenNameUi.text = chickenName;   

        petting.pettable = true;
        // petting.timer = 4; 



    }
    void CloseMenu()
    {
        menuUI.SetActive(false);
        isOpen = false;
        petting.pettable = false;

    }

    void UpdateHunger ()        //Constantly updating and checking if the hunger should go down
    {
        tHunger -= Time.deltaTime;
        if(tHunger <= 0 && hunger > 10)
        {
            tHunger = 10;
            // tHunger += 60 * 60;             // how long it should take before it drops, minute
            hunger -= 10;           
            sliderHunger.value = hunger;

        }
    }
    void UpdateThirst ()    //Constantly updating and checking if the thirst should go down
    {
        tThirst -= Time.deltaTime;
        if(tThirst <= 0)
        {
            tThirst = 10;
            thirst -= 5;
            // tThirst -= 60 * 5;           // how long it should take before it drops, minute
            sliderThirst.value = thirst;

        }
        // print (tThirst);

    }
    void UpdateHappiness ()             //Constantly updating and checking if the happiness should go down
    {
        tHappiness -= Time.deltaTime;
        if(tHappiness <= 0)
        {
            tHappiness = 60;
            happiness -= 10;
            
            // tHappiness -= 60 * 15;           // how long it should take before it drops, minute
            sliderHappiness.value = happiness;

        }

    }
}
