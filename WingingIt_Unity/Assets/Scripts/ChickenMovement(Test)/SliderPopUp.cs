//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                      A U T H O R  &  N O T E S
//                                    coded by Len, October 2019
//                        controls the food, water and veggie box level sliders
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPopUp : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    Slider slider;
    float sliderMaxValue;

    BaseBowl bowl;

    interactionConfirmation bubble;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // caching of the slider and bowl
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        sliderMaxValue = slider.maxValue;

        bowl = GetComponent<BaseBowl>();

        bubble = GetComponent<interactionConfirmation>();
    }

    // updates the slider value
    void Update()
    {
        slider.value = NormalizeAmounts();

        slider.gameObject.SetActive(bubble.clickedOnObject);
    }


    // normalizes the current bowl amount so that it fits within the slider range
    float NormalizeAmounts()
    {
        return (bowl.currentAmount * sliderMaxValue) / bowl.maxAmount;
    }
}
