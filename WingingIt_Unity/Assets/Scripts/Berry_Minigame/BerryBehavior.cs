using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBehavior : MonoBehaviour
{
    [HideInInspector] public bool beingHoveredByMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        print(gameObject.name);
        beingHoveredByMouse = true;
    }

    void OnMouseExit()
    {
        beingHoveredByMouse = false;
    }
}
