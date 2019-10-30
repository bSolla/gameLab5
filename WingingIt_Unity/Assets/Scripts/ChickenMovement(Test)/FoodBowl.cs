using UnityEngine;
using UnityEngine.UI;

public class FoodBowl : BaseBowl
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //public int avaliableFood = 5;
    // public GameObject food;
    //public int maxAvaliableFood = 100;
    //public Text foodAvaliableText;


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        currentAmount = GameManager.instance.foodBoxAmount;
    }
    
    //public void AddFood(int food)
    //{
    //    avaliableFood += food;
    //    if (avaliableFood>maxAvaliableFood)
    //    {
    //        avaliableFood = maxAvaliableFood;
    //    }
    //    // foodAvaliableText.text = "Food: " + avaliableFood;

    //}
}
