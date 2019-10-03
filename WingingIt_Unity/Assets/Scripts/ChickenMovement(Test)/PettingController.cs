//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                     A U T H O R  &  N O T E S
//                  coded by Teresa, September 2019
//      simple code for handling the petting of the chicken
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PettingController : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                             V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public ChickenStatus stat;
    public float timer = 4;
    public bool pettable;
    public ParticleSystem heartParticles;
    
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                               M E T H O D S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    void Start()
    {
        stat = GetComponent<ChickenStatus>();
        heartParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
        pettable = stat.menuUI.isMenuOpen;
        

        //resets the timer, starts the heart particles and ups the happiness stat
        if (timer <= 0)
        {
            timer = 4;
            Debug.Log ("make that chonk a happy chonk");
            stat.happiness +=10;
            heartParticles.Play();  
        }
        if(!pettable)
        {
            timer = 4;
        }
    }

    void OnMouseDrag()
    {
        // starts the timer if the chicken is pettable
        if (pettable)
        {
            timer -= Time.deltaTime;
        }
    }
}
