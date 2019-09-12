using UnityEngine;

public class FoodBowl : MonoBehaviour
{

    public int avaliableFood = 5;
    public GameObject food;

    void Update()
    {
        if(avaliableFood <= 0)
        {
            food.SetActive(false);
        }
        else
        {
            food.SetActive(true);
        }
        
    }
}
