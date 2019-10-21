using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel=1;

    public float currentExp=0;
    float necesaryExp=100;

    Text levelText;
    GameObject lvUpImage;


    // Start is called before the first frame update
    void Start()
    {
        //Should get the experience and the level from the loading script
    }

    //<<<<<<<<<<<<<<<<<<<<  When a scene is loaded (if is inside or outside) we have to call the Search Text method form the GM   >>>>>>>>>>>>>>>>>>>>>>

    public void SearchText()
    {
        levelText = GameObject.Find("Level Number").GetComponent<Text>();
        levelText.text = "" + currentLevel;

        lvUpImage = GameObject.Find("Lv Up Image");
        lvUpImage.GetComponentInChildren<Button>().onClick.AddListener(CloseLvUpImage);
        
        lvUpImage.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
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


    void LevelUp()
    {
        SearchText();
        currentLevel++;
        levelText.text = "" + currentLevel;

        lvUpImage.SetActive(true);
        print("LEVEL UP now you are "+ currentLevel);
        //Whatever this should unlook like custom assets....
    }

    void CloseLvUpImage()
    {
        lvUpImage.SetActive(false);
    }
}
