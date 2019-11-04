//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula and Len, september 2019
//              controls in which scene we are and if the chickens should be there
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public static GameManager instance = null;

    [SerializeField] GameObject chickenGroup;
    const string CHICKEN_PREFAB_FOLDER = "ChickenModels/Chicken";


    public string currentSceneName;
    public string CurrentSceneName { get => currentSceneName;}

    public int numberOfChickens;
    public List<GameObject> chickensList;

    // S T A T E   V A R I A B L E S

    bool bushIsFull=true;
    public int foodBoxAmount = 85, foodVeggieAmount = 85;
    public int waterAmount = 85;

    Chicken_Controller chickInBush;
    bool berryMinigame;
    public bool BerryMinigame { get => berryMinigame; set => berryMinigame = value; }
    public Chicken_Controller ChickInBush { get => chickInBush; set => chickInBush = value; }

    bool cutMinigame=false;
    float cuttingScore;
    public bool CutMinigame { get => cutMinigame; set => cutMinigame = value; }
    public float CuttingScore { get => cuttingScore; set => cuttingScore = value; }

    float pelletScore;
    public float PelletScore { get => pelletScore; set => pelletScore = value; }
    bool pelletMinigame;
    public bool PelletMinigame { get => pelletMinigame; set => pelletMinigame = value; }


    bool waterMinigame;
    float waterScore;
    public bool WaterMinigame { get => waterMinigame; set => waterMinigame = value; }
    public float WaterScore { get => waterScore; set => waterScore = value; }


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //When the game starts search the chickens in the first scene and add it to the list (this is only for testing the scenes we have now)
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += this.OnLoadCallback;

            InitializeAndCacheChildObjects();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Caches the chicken group object and instantiates chickens inside it 
    public void InitializeAndCacheChildObjects()
    {
        chickenGroup = gameObject.transform.GetChild(0).gameObject;

        chickensList = new List<GameObject>();
        // for (int i = 0; i < numberOfChickens; ++i)
        // {
            GameObject chick = Resources.Load(CHICKEN_PREFAB_FOLDER) as GameObject;
            chickensList.Add(Instantiate(chick, chickenGroup.transform.position, chickenGroup.transform.rotation));
            chickensList[0].transform.parent = chickenGroup.transform;
            // StatusMenuUI.FindObjectOfType<StatusMenuUI>().ChangeChickenName(chick.GetComponent<ChickenStatus>());


        // }

        ActivateChickensToggle();
    }


    // Destroys all chickens inside the chicken list 
    public void ClearChickenGroup()
    {
        foreach (GameObject chick in chickensList)
        {
            Destroy(chick);
        }

        chickensList.Clear();
    }


    //Each time a level is load the manager checks if we are in the coop or not and activate the chickens if they are suposed to be there
    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        currentSceneName = scene.name;

        LoadStatsBetweenScenes();
        ActivateChickensToggle();


        //<<<<<<<<<<<<<<<<<<     This is to make the level manager work while changing scenes     >>>>>>>>>>>>>>>>>>
        GetComponent<LevelManager>().SearchText();

        GetComponent<EggManager>().RefreshScene();
    }


    //This method check the current location of the chickens and call the method ActivateChicken in the ones which are in the current scene
    void ActivateChickensToggle()
    {
        if (CurrentSceneName == "Inside" || CurrentSceneName == "Outside")                 //Put the names of the scenes we are using here!!!!!!!
        {
            chickenGroup.SetActive(true);
        }
        else
        {
            chickenGroup.SetActive(false);
        }

        foreach (GameObject chick in chickensList)
        {
            if (chick.GetComponent<Chicken_Controller>().CurrentLocation == CurrentSceneName)
            {
                chick.GetComponent<Chicken_Controller>().ActivateChicken();
            }
            else
            {
                chick.GetComponent<Chicken_Controller>().DeactivateChicken();
            }

            chick.GetComponent<ChickenStatus>().SearchReferences();
        }
    }


    void LoadStatsBetweenScenes()
    {
        if (CurrentSceneName == "Inside")
        {
            FindObjectOfType<FoodBowl>().currentAmount = foodBoxAmount;
            FindObjectOfType<FoodBowl>().AddAmount(0);

            FindObjectOfType<WaterDispenser>().currentAmount = waterAmount;

            if (pelletMinigame)
            {
                pelletMinigame = false;
                foodBoxAmount += (int)pelletScore;
                FindObjectOfType<FoodBowl>().currentAmount = foodBoxAmount;
                // Debug.Log ("food amount: " + foodBoxAmount);
            }
            if (waterMinigame)
            {
                waterMinigame = false;
                
                waterAmount += (int)waterScore;    
                FindObjectOfType<WaterDispenser>().AddWater(waterAmount);
                // Debug.Log ("water amount: " + waterAmount);
                //FindObjectOfType<WaterDispenser>().AddFood(0);               
            }
        }

        if (CurrentSceneName == "Outside")
        {
            if (berryMinigame)
            {
                berryMinigame = false;

                // done in the berry manager
                //ChickInBush.GetComponent<ChickenStatus>().hunger += 30;
            }

            if (cutMinigame)
            {
                cutMinigame = false;
                
                foodVeggieAmount += (int)cuttingScore;
                //// print("Veggie food: " + (int)cuttingScore / 10);    
                //FindObjectOfType<FoodBowl>().currentAmount = foodVeggieAmount;
                //FindObjectOfType<FoodBowl>().AddAmount(0);               
            }

            FindObjectOfType<BerryBush>().bushFull = bushIsFull;
            FindObjectOfType<VeggieBowl>().currentAmount = foodVeggieAmount;

            
            //waterAmount
        }
    }


    public void SaveStatsBetweenScenes()   //Change this things to the scenes they actually are
    {
        if (CurrentSceneName=="Inside")
        {
            foodBoxAmount = FindObjectOfType<FoodBowl>().currentAmount;
            waterAmount = FindObjectOfType<WaterDispenser>().currentAmount;

        }

        if (CurrentSceneName=="Outside")
        {
            bushIsFull = FindObjectOfType<BerryBush>().bushFull;
            //foodVeggieAmount = FindObjectOfType<FoodBowl>().currentAmount;

            //waterAmount
        }
    }


    // returns a list of ChickenStatusValues for the chickens
    public List<ChickenStatusValues> GetChickenStatusList()
    {
        List<ChickenStatusValues> chickenStatusList = new List<ChickenStatusValues>(); ;

        foreach (GameObject chick in chickensList)
        {
            ChickenStatus chickStatus = chick.GetComponent<ChickenStatus>();
            ChickenStatusValues chickValues = new ChickenStatusValues();

            chickValues.hunger = chickStatus.hunger;
            chickValues.thirst = chickStatus.thirst;
            chickValues.happiness = chickStatus.happiness;
            chickValues.currentState = chickStatus.currState;
            chickValues.name = chickStatus.chickenName;

            chickenStatusList.Add(chickValues);
        }

        return chickenStatusList;
    }

    
    // updates the values of the chicken status of each chicken in the chicken list
    public void LoadChickenStatusValues(List<ChickenStatusValues> chickenStatusValues)
    {
        for (int i = 0; i < numberOfChickens; ++i)
        {
            ChickenStatus chickStatus = chickensList[i].GetComponent<ChickenStatus>();

            chickStatus.hunger = chickenStatusValues[i].hunger;
            chickStatus.thirst = chickenStatusValues[i].thirst;
            chickStatus.happiness = chickenStatusValues[i].happiness;

            chickStatus.currState = chickenStatusValues[i].currentState;

            chickStatus.chickenName = chickenStatusValues[i].name;
        }
    }


    public void GetNewChicken()
    {
        GameObject chickenGroupObj = this.transform.GetChild(0).gameObject;
        GameObject chick = Resources.Load(CHICKEN_PREFAB_FOLDER) as GameObject;
        GameObject newChick = Instantiate(chick, chickenGroupObj.transform.position, chickenGroupObj.transform.rotation);
        chickensList.Add(newChick);
        numberOfChickens ++;
        // chickensList.Add(Instantiate(chick, chickenGroupObj.transform.position, chickenGroupObj.transform.rotation));
        // print(chick);
        newChick.transform.parent = chickenGroupObj.transform;
        
        // StatusMenuUI.FindObjectOfType<StatusMenuUI>().ChangeChickenName(chick.GetComponent<ChickenStatus>());

        // ActivateChickensToggle();

    }
}