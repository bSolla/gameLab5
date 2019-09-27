using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatEgg : MonoBehaviour
{
    //whatEgg is a show if the egg should be common(1), rare (2) or legendary(3). If the egg haven't gotten anything, it is a 0.
    public float whatEgg; 
    private Renderer _rend;


    public Material[] commonMat, rareMat, legendaryMat;


    void Start()
    {
        _rend = GetComponentInChildren<Renderer>();

        float rndEggValue = UnityEngine.Random.value;


        if(rndEggValue >= 0.4f)      //Common - 60% Drop rate
        {
            print ("I dropped a common egg");
            whatEgg = 1;
        }
        if(rndEggValue > 0.1f && whatEgg < 0.4f)        // Rare - 30% Drop rate
        {
            print ("I dropped a rare egg");
            whatEgg = 2;
        }
        if(rndEggValue <= 0.1f)             //Legendary - 10% drop rate
        {
            print ("I dropped a legendary egg");
            whatEgg = 3;
        }



        if(whatEgg == 1)
        {
            _rend.material = commonMat[Random.Range(0, commonMat.Length)];
        }
        if(whatEgg == 2)
        {
            _rend.material = rareMat[Random.Range(0, rareMat.Length)];
        }  
        if(whatEgg == 3)
        {
            _rend.material = legendaryMat[Random.Range(0, legendaryMat.Length)];
        }       

    }
}
