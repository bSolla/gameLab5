using UnityEngine;
using UnityEngine.UI;

public class FoodBowl : MonoBehaviour
{

    public int avaliableFood = 5;
    public GameObject food;
    public Text foodAvaliableText;

    // void Start()
    // {
    // }
    void Update()
    {
        foodAvaliableText.text = "Food: " + avaliableFood;
        
        if(avaliableFood <= 0)
        {
            food.SetActive(false);
        }
        else
        {
            food.SetActive(true);
        }

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
                avaliableFood += 10;
            }
        }
    }
}
