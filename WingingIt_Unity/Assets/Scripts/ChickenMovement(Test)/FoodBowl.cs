using UnityEngine;
using UnityEngine.UI;

public class FoodBowl : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public int avaliableFood = 5;
    // public GameObject food;
    // public Text foodAvaliableText;

    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Update()
    {
        // foodAvaliableText.text = "Food: " + avaliableFood;
        
        // if(avaliableFood <= 0)
        // {
        //     food.SetActive(false);
        // }
        // else
        // {
        //     food.SetActive(true);
        // }

        if (Input.GetMouseButtonUp(0) && avaliableFood <= 100)
        {
            fillFood();
        }
    }
    void fillFood()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Collider col = this.gameObject.GetComponent<Collider>();
        if(Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider == col)
            {
                if(this.gameObject.tag == "VegetableFeeder")
                {
                    GetComponent<ChangingScenes>().GoToScene("CuttingMinigame");
                }
                else if(this.gameObject.name == "Feeder")
                {
                    avaliableFood += 10;

                }
            }
        }
    }
    public void AddFood(int food)
    {
        avaliableFood += food;
        if (avaliableFood>100)
        {
            avaliableFood = 100;
        }
        // foodAvaliableText.text = "Food: " + avaliableFood;

    }
}
