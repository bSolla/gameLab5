using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBehavior : MonoBehaviour
{
    [HideInInspector] public bool beingHoveredByMouse = false;
    private ParticleSystem feedbackParticles;
    [SerializeField] private float feedbackParticlesPlaybackSpeed = 5f;

    void Awake()
    {
        feedbackParticles = GetComponentInChildren<ParticleSystem>();
        feedbackParticles.playbackSpeed = feedbackParticlesPlaybackSpeed;
    }


    void OnMouseOver()
    {
        beingHoveredByMouse = true;
    }


    void OnMouseExit()
    {
        beingHoveredByMouse = false;
    }


    public void ActivateFeedbackParticles()
    {
        feedbackParticles.Play();
    }
}
