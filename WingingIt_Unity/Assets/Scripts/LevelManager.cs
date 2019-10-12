using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int currentLevel=1;

    float currentExp=0;
    float necesaryExp=100;

    // Start is called before the first frame update
    void Start()
    {
        //Should get the experience and the level from the loading script
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
        currentLevel++;
        print("LEVEL UP now you are "+ currentLevel);
        //Whatever this should unlook like custom assets....
    }
}
