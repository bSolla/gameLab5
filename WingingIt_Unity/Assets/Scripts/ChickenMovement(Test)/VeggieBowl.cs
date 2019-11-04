using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieBowl : BaseBowl
{
    // Start is called before the first frame update
    void Start()
    {
        currentAmount = GameManager.instance.foodVeggieAmount;
    }
    
}
