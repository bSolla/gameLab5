//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              updates the stats of the selected chicken in the menu panel
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenuUI : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Slider hungerSlider, thirstSlider, happinessSlider;
    Text nameText;
    GameObject panel;
    public GameObject Panel { get => panel;}
    float delay=0;

    ChickenStatus currentChicken;

    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        panel = transform.Find("Panel").gameObject;

        hungerSlider=transform.Find("Panel/HungerSlider").GetComponent<Slider>();
        thirstSlider = transform.Find("Panel/ThirstSlider").GetComponent<Slider>();
        happinessSlider = transform.Find("Panel/HappinessSlider").GetComponent<Slider>();
        nameText = transform.Find("Panel/Name").GetComponent<Text>();

        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Panel.activeSelf)
        {
            hungerSlider.value = currentChicken.hunger;
            thirstSlider.value = currentChicken.thirst;
            happinessSlider.value = currentChicken.happiness;

            if (delay > 0)
            { delay -= Time.deltaTime; }
            else if (Input.GetMouseButtonUp(0))
            {
                CloseMenu();
            }
        }
    }

    public void OpenMenu(ChickenStatus chick)
    {
        currentChicken = chick;
        Panel.SetActive(true);
        nameText.text = currentChicken.chickenName;
        delay = 0.5f;
    }

    void CloseMenu()
    {
        Panel.SetActive(false);
    }
}
