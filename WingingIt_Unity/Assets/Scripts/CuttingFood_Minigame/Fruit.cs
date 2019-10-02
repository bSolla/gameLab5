//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//              controls the rotation, when it is sliced and adds score
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    [SerializeField] GameObject slicePref;
    [SerializeField] float points=10;
    float speedRotate;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Calculates a random rotation
    private void Start()
    {
        float num = Random.Range(0, 2);
        if (num == 1)
        { speedRotate = Random.Range(30, 60);}
        else { speedRotate = Random.Range(-60, -30);}
    }


    //Applies the rotation
    private void Update()
    {
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }


    //When it enters on trigger with the knife the gameobject is destroyed and instantiate the slices, also calls the manager to change the score
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Knife")
        {
            GameObject slices= Instantiate(slicePref,this.transform.position, this.transform.rotation);
            Destroy(slices, 2f);

            FindObjectOfType<CuttingGameManager>().AddPoints(points);

            Destroy(this.gameObject);
        }
    }  
}
