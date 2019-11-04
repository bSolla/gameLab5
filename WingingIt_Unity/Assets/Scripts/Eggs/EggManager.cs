using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EggManager : MonoBehaviour
{
    EggInfo[] commonEggs;
    EggInfo[] rareEggs;
    EggInfo[] legendaryEggs;

    bool eggDroped;
    bool eggPicked;
    EggInfo eggInfo;
    [HideInInspector] public GameObject currentEgg;    

    LevelManager lm;

    [SerializeField] float commonExp=20;
    [SerializeField] float rareExp = 50;
    [SerializeField] float legendaryExp = 100;

    //Kine variables
    public DateTime currentTime, oldTime;
    public GameObject eggPrefab;
    Transform[] dropTrans;


    public EggInfo[] CommonEggs { get => commonEggs; set => commonEggs = value; }
    public EggInfo[] RareEggs { get => rareEggs; set => rareEggs = value; }
    public EggInfo[] LegendaryEggs { get => legendaryEggs; set => legendaryEggs = value; }


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private void Awake()
    {
        Material[] commonMaterials = Resources.LoadAll<Material>("Common Materials");
        Material[] rareMaterials = Resources.LoadAll<Material>("Rare Materials");
        Material[] legendaryMaterials = Resources.LoadAll<Material>("Legendary Materials");

        CommonEggs = new EggInfo[commonMaterials.Length];
        RareEggs = new EggInfo[rareMaterials.Length];
        LegendaryEggs = new EggInfo[legendaryMaterials.Length];

        int i = 0; 
        foreach (Material mat in commonMaterials)
        {
            CommonEggs[i] = new EggInfo()
            {
                eggMaterial = mat,
                type = EggInfo.EggType.Common,
                owned = false,
            };
            i++;
        }

        i = 0; 
        foreach (Material mat in rareMaterials)
        {
            RareEggs[i] = new EggInfo();
            RareEggs[i].eggMaterial = mat;
            RareEggs[i].type = EggInfo.EggType.Rare;
            RareEggs[i].owned = false;
            i++;
        }

        i = 0; 
        foreach (Material mat in legendaryMaterials)
        {
            LegendaryEggs[i] = new EggInfo();
            LegendaryEggs[i].eggMaterial = mat;
            LegendaryEggs[i].type = EggInfo.EggType.Legendary;
            LegendaryEggs[i].owned = false;
            i++;
        }
    }

    //When you change scenes eggs disapear so the scripts thinks that there is an egg when its not, so each time a scene is loaded the bool is set to false
     public void RefreshScene()  //Maybe better if we call this from the gm, I dont want to do it now because it may overwritte
     {
        eggDroped = false; //If we make the eggs apears always in the nest we can just remove this method but I'll keep ot here for now
        eggPicked = false;

        if (GameManager.instance.CurrentSceneName=="Inside")
        {
            SearchSpawnPoints();
        }
     }


    void SearchSpawnPoints()
    {
        GameObject[] nestObjects = GameObject.FindGameObjectsWithTag("NestSpawn");
        dropTrans = new Transform[nestObjects.Length];
        for (int i = 0; i < nestObjects.Length; i++)
        {
                dropTrans[i] = nestObjects[i].transform;
        }
    }

    void Start()
    {
        print(System.DateTime.Now);
        oldTime = DateTime.Now;

        if (GameManager.instance.CurrentSceneName == "Inside")
        {
            SearchSpawnPoints();
        }

        lm = GetComponent<LevelManager>();
    }


    void Update()
    {
        if(GameManager.instance.currentSceneName == "Inside")
        {
            if(!eggDroped && DropTheEgg())
            {
                DropAnEgg();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (eggPicked)
                {
                    Destroy(currentEgg);
                    eggPicked = false;
                }

                if (eggDroped  && !lm.lvUpImage.activeInHierarchy)
                {
                    PickUpEgg();
                }            
            }
            
        }        
    }


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<       P I C K   U P   E G G S   M E T H O D S       >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void PickUpEgg()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag=="Egg")
            {
                currentEgg.transform.localScale *= 3;
                currentEgg.transform.position = new Vector3(0, 5, 0);

                EggInfo info = currentEgg.GetComponent<EggInfo>();
                AddToColection(info);
                oldTime = currentTime;

                switch (info.type)
                {
                    case EggInfo.EggType.Common:
                        lm.AddExp(commonExp);
                        break;

                    case EggInfo.EggType.Rare:
                        lm.AddExp(rareExp);
                        break;

                    case EggInfo.EggType.Legendary:
                        lm.AddExp(legendaryExp);
                        break;

                    default:
                        break;
                }

                //Change this to another way to destroy them, zoom with the camera, bool that you are looking at them and a new method to rotate them
                eggDroped = false;
                eggPicked = true;
            }
        }
    }



    void AddToColection(EggInfo pickedEgg)
    {
        if (!pickedEgg.owned)
        {
            switch (pickedEgg.type)
            {
                case EggInfo.EggType.Common:
                    CommonEggs[pickedEgg.eggNum].owned = true;
                    break;

                case EggInfo.EggType.Rare:
                     RareEggs[pickedEgg.eggNum].owned = true;
                    break;

                case EggInfo.EggType.Legendary:
                     LegendaryEggs[pickedEgg.eggNum].owned = true;
                    break;

                default:
                    break;
            }
        }
    }



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<        E G G   D R O P   M E T H O D S      >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


    //Checks if its time to drop an egg
    // private void CheckNewTime()
    // {
    //     // string currScene = GetComponent<GameManager>().currentSceneName;
    //     // currentTime = DateTime.Now;
    //     if ( && GameManager.instance.CurrentSceneName == "Inside")
    //     {
    //         // if (oldTime.Date < currentTime.Date)
    //         // {
    //         //     return;
    //         // }
    //         // else
    //         // {
    //         //     if (oldTime.Hour < currentTime.Hour)
    //         //     {
    //         //         print ("Hour");
    //         //         DropAnEgg();
    //         //     }
    //         //     else
    //         //     {
    //         //         if (currentTime.Minute - oldTime.Minute >= 0.1f || Input.GetKeyDown(KeyCode.E)) // ????????? Check this later
    //         //         {
    //         //             print ("Minute");

    //         //             DropAnEgg();
    //         //         }
    //         //     }
    //         // }
            
    //     }
    // }

    bool DropTheEgg()
    {
        currentTime = DateTime.Now;
        
        if(Input.GetMouseButtonDown(1))
        {
            print("Curr minute: " + currentTime.Minute);
            
            print("old minute: " + oldTime.Minute);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
          return true;  
        }


        if(oldTime.Hour < currentTime.Hour)
        {
            return true;
        }
        if(currentTime.Minute - oldTime.Minute >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }

    //Instantiates an egg
    private void DropAnEgg()
    {
            
            eggDroped=true;
        // oldTime = currentTime;

        // dropTrans = GameObject.FindGameObjectWithTag("Chicken").transform.position;
        // dropTrans = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.position;
        // dropTrans = new Vector3(dropTrans.x + 2, 0.5f, dropTrans.z + 2);

        int num = UnityEngine.Random.Range(0, GameManager.instance.numberOfChickens - 1);
        print(num);
        Vector3 myDropTrans = dropTrans[num].position;        

        GameObject newEgg = Instantiate(eggPrefab, myDropTrans, transform.rotation);
        eggInfo=newEgg.GetComponent<EggInfo>();
        ChooseRandomEgg();
        currentEgg = newEgg;        

            //GetComponent<EggPickUp>().eggCol = newEgg.GetComponent<Collider>();
        
    }

    
    //First it decides the egg type (common, rare...), then choose a random egg from the array and sets the variables to the egg in the scene
    void ChooseRandomEgg()
    {
        //_rend = GetComponentInChildren<Renderer>();

        float rndEggValue = UnityEngine.Random.value;


        if (rndEggValue >= 0.4f)      //Common - 60% Drop rate
        {
            print("I dropped a common egg");
            eggInfo.type = EggInfo.EggType.Common;
        }
        else if (rndEggValue > 0.1f)        // Rare - 30% Drop rate
        {
            print("I dropped a rare egg");
            eggInfo.type = EggInfo.EggType.Rare;
        }
        else //Legendary - 10% drop rate
        {
            print("I dropped a legendary egg");
            eggInfo.type = EggInfo.EggType.Legendary;
        }



        if (eggInfo.type == EggInfo.EggType.Common)
        {
            int num = UnityEngine.Random.Range(0, CommonEggs.Length);
            eggInfo.eggMaterial = CommonEggs[num].eggMaterial;
            eggInfo.owned= CommonEggs[num].owned;
            eggInfo.eggNum = num;
        }
        else if (eggInfo.type == EggInfo.EggType.Rare)
        {
            int num = UnityEngine.Random.Range(0, RareEggs.Length);
            eggInfo.eggMaterial = RareEggs[num].eggMaterial;
            eggInfo.owned = RareEggs[num].owned;
            eggInfo.eggNum = num;
        }
        else
        {
            int num = UnityEngine.Random.Range(0, LegendaryEggs.Length);
            eggInfo.eggMaterial = LegendaryEggs[num].eggMaterial;
            eggInfo.owned = LegendaryEggs[num].owned;
            eggInfo.eggNum = num;
        }
    }
}
