using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusMenu : MonoBehaviour
{
    public enum State {Normal, Hungry, Thirsty, Sad};
    public State currState = State.Normal;
    public int hunger = 100, thirst = 100, happiness = 100;
    private float tHunger, tThirst, tHappiness;
    public GameObject menuUI;
    // public float realTime;
    // public DateTime currTime, lastTime;
    private Text chickenNameUi;
    public String chickenName;
    public Slider sliderHunger, sliderThirst, sliderHappiness;
    bool isOpen, gotUI;

    ChickenController chickenController;
    FoodBowl food;
    WaterDispenser water;

    // public bool isHungry;


    void Start()
    {
        chickenController = GetComponent<ChickenController>();
        food = chickenController.foodBowl.GetComponent<FoodBowl>();

        CloseMenu();
        // print (hunger + " " + thirst + " " + happiness);

        tHunger = 10;
    }

    

    void Update()
    {
        if (!gotUI)
        {
            getUI();
        }
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
                    // print ("Hit? " + gameObject.name);

                    OpenMenu();
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
        if(hunger < 60)
        {
            currState = State.Hungry;
        }
        if(thirst < 50)
        {
            currState = State.Thirsty;
            // chickenController.movingPoint();
        }
        if(happiness < 50)
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
            if(hunger >= 50)
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

    void OpenMenu()
    {
        menuUI.SetActive(true);
        isOpen = true;
        sliderHunger.value = hunger;
        sliderThirst.value = thirst;
        sliderHappiness.value = happiness;
        chickenNameUi.text = chickenName;   

    }
    void CloseMenu()
    {
        menuUI.SetActive(false);
        isOpen = false;
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

        
        // print (tHunger);

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
            tHappiness = 10;
            happiness -= 10;
            
            // tHappiness -= 60 * 15;           // how long it should take before it drops, minute
            sliderHunger.value = happiness;

        }
        // print (tHunger);

    }

    private void getUI ()
    {
        menuUI = GameObject.Find("Canvas/StatusMenu");
        chickenNameUi = GameObject.Find("Name").GetComponent<Text>();
        sliderHappiness = GameObject.Find("HappinessSlider").GetComponent<Slider>();
        sliderHunger = GameObject.Find("Hunger slider").GetComponent<Slider>();
        sliderThirst = GameObject.Find("ThirstSlider").GetComponent<Slider>();
        gotUI = true;
        Debug.Log ("setting ui");
    } 


}
