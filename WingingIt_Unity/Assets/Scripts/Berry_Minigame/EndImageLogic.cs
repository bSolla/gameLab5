using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndImageLogic : MonoBehaviour
{
    [SerializeField] float timeSpentFading = 1.5f;

    Color spriteColor;

    float opaqueAlpha = 1f;
    float transparentAlpha = 0f;
    
    public void StartFading()
    {
        StartCoroutine(FadeTo(timeSpentFading));
    }


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
