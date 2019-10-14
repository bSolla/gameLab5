//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              spawns a randomly gameobjects from an array and applies force
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    [SerializeField] GameObject[] food;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float maxNumElements;

    [SerializeField] float maxTime;
    [SerializeField] float minTime;
    
    [SerializeField] float maxForce;
    [SerializeField] float minForce;

    float timeNext;
    float timeAct;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Calculates random number of elements to spawn each time and a random time for the next spawns, when it spawns a random gameobject from the array it also applies force to it
    void Update()
    {
        if (timeAct>=timeNext)
        {
            float num =Random.Range(1, maxNumElements);

            timeNext = Random.Range(minTime, maxTime);
            timeAct = 0;

            for (int i = 0; i < num; i++)
            {
                GameObject f=Instantiate(food[Random.Range(0,food.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

                Vector2 dir = this.transform.position-f.transform.position + new Vector3(0, Random.Range(-1, 1),0); //Calcular la direccion
                f.GetComponent<Rigidbody2D>().AddForce(dir*Random.Range(minForce,maxForce)*0.1f,ForceMode2D.Impulse);

                Destroy(f,2);
            }
        }

        timeAct += Time.deltaTime;
    }
}
