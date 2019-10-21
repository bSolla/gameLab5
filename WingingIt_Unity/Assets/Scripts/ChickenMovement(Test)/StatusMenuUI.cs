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

    public Slider hungerSlider, thirstSlider, happinessSlider;
    Text nameText;
    public InputField inputField;
    GameObject panel;
    public GameObject Panel { get => panel;}
    float delay=0;

    public bool isMenuOpen;

    ChickenStatus currentChicken;
    Camera cam;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        cam = Camera.main;

        panel = transform.GetChild(0).gameObject;

        Panel.SetActive(false);
        isMenuOpen = false;

        // ChangeChickenName(g);
    }

    // Update is called once per frame
    void Update()
    {
        if (Panel.activeSelf)
        {
            hungerSlider.value = currentChicken.hunger;
            thirstSlider.value = currentChicken.thirst;
            happinessSlider.value = currentChicken.happiness;

            currentChicken.GetComponent<Chicken_Controller>().LookAtPlayer();


            if (delay > 0)
            { delay -= Time.deltaTime; }
            
            else if (Input.GetMouseButtonUp(0))
            {
                if(currentChicken.chickenName != null )
                {
                    CloseMenu();

                }
                else
                {
                    ChangeChickenName(currentChicken);

                }
            }
        }
        if(hungerSlider == null || thirstSlider == null || happinessSlider == null || nameText == null)
        {
            FindSliders();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnterChickenName();
        }
    }

    public void OpenMenu(ChickenStatus chick)
    {
        currentChicken = chick;
        Panel.SetActive(true);
        nameText.text = currentChicken.chickenName;
        delay = 0.5f;
        isMenuOpen = true;
        cam.GetComponent<CameraFollow>().startFollowing(currentChicken.transform);

    }

    void CloseMenu()
    {
        Panel.SetActive(false);
        isMenuOpen = false;
        cam.GetComponent<CameraFollow>().stopFollowing();
        currentChicken.GetComponent<Chicken_Controller>().canMove = true;



    }
    
    void FindSliders()
    {
        hungerSlider= panel.transform.GetChild(0).GetComponent<Slider>();
        thirstSlider = panel.transform.GetChild(1).GetComponent<Slider>();
        happinessSlider = panel.transform.GetChild(2).GetComponent<Slider>();
        nameText = panel.transform.GetChild(3).GetComponent<Text>();
    }
    public void ChangeChickenName(ChickenStatus chick)
    {
        
        OpenMenu(chick);
        nameText.text = " ";
        inputField.gameObject.SetActive(true);
        // Time.timeScale = 0;


    }
    public void EnterChickenName()
    {
        // Time.timeScale = 1;
        ChickenStatus chick = GameManager.instance.chickensList[GameManager.instance.chickensList.Count -1].GetComponent<ChickenStatus>();
        inputField.gameObject.SetActive(false);
        chick.chickenName = inputField.text;
        nameText.text = chick.chickenName;
        inputField.text = null;
    }
}
