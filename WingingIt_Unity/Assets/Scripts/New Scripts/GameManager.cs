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
        }
        else
        {
            chickenGroup.SetActive(false);
        }

        foreach (GameObject chick in chickensList)
        {
            if (chick.GetComponent<ChickenController>().CurrentLocation == CurrentSceneName)
            {
                chick.GetComponent<ChickenController>().ActivateChicken();
            }
            else
            {
                chick.GetComponent<ChickenController>().DesactivateChicken();    
            }

            chick.GetComponent<ChickenController>().DoorPoint = GameObject.Find("DoorPoint").transform.position;
            chick.GetComponent<ChickenStatus>().SearchReferences();
        }     
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
