using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject chickenGroup;

    string currentSceneName;
    public string CurrentSceneName { get => currentSceneName;}

    List<GameObject> chickensList;
   

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        chickensList = new List<GameObject>();
        GameObject[] array = GameObject.FindGameObjectsWithTag("Chicken");

        foreach (GameObject chick in array)
        {
            chickensList.Add(chick);
        }

        chickenGroup.SetActive(false);
    }

    private void OnLevelWasLoaded(int level)
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if (CurrentSceneName == "Inside"|| CurrentSceneName == "Outside")                 //Put the names of the scenes we are using here!!!!!!!
        {
            chickenGroup.SetActive(true);
            ActivateChickens();
        }
        else
        {
            chickenGroup.SetActive(false);
        }    
    }

    void ActivateChickens()
    {
        foreach (GameObject chick in chickensList)
        {
            if (chick.GetComponent<Chicken_Controller>().CurrentLocation == CurrentSceneName)
            {
                chick.GetComponent<Chicken_Controller>().ActivateChicken();
            }
            else
            {
                chick.GetComponent<Chicken_Controller>().DesactivateChicken();
            }

            chick.GetComponent<Chicken_Controller>().DoorPoint = GameObject.Find("DoorPoint").transform.position;
            chick.GetComponent<ChickenStatus>().SearchReferences();
        }
    }
}
