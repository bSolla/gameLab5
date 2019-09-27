//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Len, september 2019
//  simple logic for the end of puzzle image that allows it to fade in and out
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndImageLogic : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    [SerializeField] float timeSpentFading = 1.5f;

    Color spriteColor;

    float opaqueAlpha = 1f;
    float transparentAlpha = 0f;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // public method (used from external classes) to start the fading process
    public void StartFading()
    {
        StartCoroutine(FadeTo(timeSpentFading));
    }


    // manages the fade in and out, taking the fading time into account
    IEnumerator FadeTo(float aTime)
    {
        // from transparent to opaque
        for (float relativeTime = 0.0f; relativeTime < 1.0f; relativeTime += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(transparentAlpha, opaqueAlpha, relativeTime));
            GetComponent<SpriteRenderer>().color = newColor;

            yield return null;
        }

        // from opaque to transparent
        for (float relativeTime = 0.0f; relativeTime < 1.0f; relativeTime += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(opaqueAlpha, transparentAlpha, relativeTime));
            GetComponent<SpriteRenderer>().color = newColor;

            yield return null;
        }
    }
}
