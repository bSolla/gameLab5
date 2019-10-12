using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlgorithm : MonoBehaviour
{
    float exp=100;
    float level=1;
    public float mult=1.1f;


    // Start is called before the first frame update
    void Start()
    {
        /*while (level<100)
        {
            print(level+": "+exp);
            exp += exp * mult;
            level++;
        }*/

        while (level < 100)
        {
            print(level + ": " + exp);
            exp = (int)exp * mult;
            level++;
        }
    }
}
