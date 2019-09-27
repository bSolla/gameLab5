//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Len, september 2019
//  individual behavior for berries in the conect the dots minigame, mostly checks wether
//  or not the mouse is over them, having a public bool that reflects on that. Also has 
//  a public method to play its feedback particles
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BerryBehavior : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [HideInInspector] public bool beingHoveredByMouse = false;
    private ParticleSystem feedbackParticles;
    [SerializeField] private float feedbackParticlesPlaybackSpeed = 5f;


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // caching of the particle system component, and setting the right playback speed
    void Awake()
    {
        feedbackParticles = GetComponentInChildren<ParticleSystem>();
        feedbackParticles.playbackSpeed = feedbackParticlesPlaybackSpeed;
    }


    // sets the public bool beingHoveredByMouse to true
    void OnMouseOver()
    {
        beingHoveredByMouse = true;
    }


    // sets the public bool beingHoveredByMouse to false
    void OnMouseExit()
    {
        beingHoveredByMouse = false;
    }


    // plays the particles
    public void ActivateFeedbackParticles()
    {
        feedbackParticles.Play();
    }
}
