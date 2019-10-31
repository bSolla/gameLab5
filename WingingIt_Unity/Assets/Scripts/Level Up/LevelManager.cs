using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public int currentLevel=1;

    public float currentExp=0;
    float necesaryExp=100;

    Text levelText;
    public GameObject lvUpImage;

    LevelReguards lr;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        lr = GetComponent<LevelReguards>();

        //Should get the experience and the level from the loading script
    }

    //<<<<<<<<<<<<<<<<<<<<  When a scene is loaded (if is inside or outside) we have to call the Search Text method form the GM   >>>>>>>>>>>>>>>>>>>>>>

    public void SearchText()
    {
        levelText = GameObject.Find("Level Number").GetComponent<Text>();
        levelText.text = "" + currentLevel;

        if(lvUpImage == null)
        {
            lvUpImage = GameObject.Find("Lv Up Image");

        }
        lvUpImage.GetComponentInChildren<Button>().onClick.AddListener(CloseLvUpImage);
        
        lvUpImage.SetActive(false);

    }




    public void AddExp(float exp)
    {
        currentExp += exp;

        if (currentExp>=necesaryExp)
        {
            currentExp -= necesaryExp;
            necesaryExp = (int)necesaryExp * 1.1f;

            LevelUp();
        }
    }

    //Remember to show the sprites of the reguards
    void LevelUp()
    {
        SearchText();
        currentLevel++;
        levelText.text = "" + currentLevel;

        lvUpImage.SetActive(true);
        print("LEVEL UP now you are "+ currentLevel);

        //Whatever this should unlook like custom assets....
        List<LevelReguards.Reguard> rgList = lr.GetReguards(currentLevel);

        foreach (LevelReguards.Reguard rg in rgList)
        {
            print(rg.lv); print(rg.type);
        }
    }

    void CloseLvUpImage()
    {
        lvUpImage.SetActive(false);
    }
}
