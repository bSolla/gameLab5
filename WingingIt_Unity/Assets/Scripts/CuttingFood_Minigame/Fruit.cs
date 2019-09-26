using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] GameObject slicePref;
    [SerializeField] float points=10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Knife")
        {
            //Vector3 dir = (other.transform.position - this.transform.position).normalized;
            //Quaternion rot = Quaternion.LookRotation(dir);

            GameObject slices= Instantiate(slicePref,this.transform.position, Quaternion.identity);//rot
            Destroy(slices, 1.5f);

            FindObjectOfType<CuttingGameManager>().AddPoints(points);

            Destroy(this.gameObject);
        }
    }  
}
