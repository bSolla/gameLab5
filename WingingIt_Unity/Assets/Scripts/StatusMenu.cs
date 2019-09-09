using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusMenu : MonoBehaviour
{
    public int hunger = 100, thirst = 100, happiness = 100;
    float tHunger = 60, tThirst = 60, tHappiness = 60;
    public GameObject menuUI;
    // public float realTime;
    // public DateTime time;

    public Slider sliderHunger, sliderThirst, sliderHappiness;
    public bool isOpen;


    void Start()
    {
        CloseMenu();   
    }

    void Update()
    {
        if(isOpen)
        {
            UpdateHunger();
            UpdateThirst();
            UpdateHappiness();    
        }
        

        // realTime = DateTime.Now;
        // time = DateTime.Now;
        // print(time.TimeOfDay);
        
        if(isOpen)
        {
            if(Input.GetMouseButtonDown(0))
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
                if(hit.collider == col && Input.GetMouseButtonDown(0))
                {
                    print ("Hit? " + gameObject.name);

                    OpenMenu();
                }
            }
        }
    }

    void OpenMenu()
    {
        menuUI.SetActive(true);
        isOpen = true;
        sliderHunger.value = hunger;
        sliderThirst.value = thirst;
        sliderHunger.value = happiness;



    }
    void CloseMenu()
    {
        menuUI.SetActive(false);
        isOpen = false;
    }

    void UpdateHunger ()
    {
        tHunger -= Time.deltaTime;
        if(tHunger <= 0)
        {
            tHunger = 5;
            hunger -= 50;
        }
        // print (tHunger);
        sliderHunger.value = hunger;

    }
    void UpdateThirst ()
    {
        tThirst -= Time.deltaTime;
        if(tThirst <= 0)
        {
            tThirst = 5;
            thirst -= 50;
        }
        // print (tThirst);
        sliderThirst.value = thirst;

    }
    void UpdateHappiness ()
    {
        tHappiness -= Time.deltaTime;
        if(tHappiness <= 0)
        {
            tHappiness = 5;
            happiness -= 50;
        }
        // print (tHunger);
        sliderHunger.value = happiness;

    }
}
