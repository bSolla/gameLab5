using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    StatusMenu statusMenu;
    public float planeX, planeZ;
    Vector3 walkPoint;

    void Start()
    {
        planeX = GameObject.FindWithTag("Ground").transform.localScale.x;
        planeZ = GameObject.FindWithTag("Ground").transform.localScale.z;
    }

    void Update()
    {
        
        print (walkPoint);
        if(Vector3.Distance(transform.position, walkPoint) > 0.0001f)
        {
            print ("Distance is far");
            transform.Translate(walkPoint, Space.Self);
            
        }
        else
        {
            float rndX = Random.Range(0, planeX); print (rndX);
            float rndZ = Random.Range(0, planeZ);
            walkPoint = new Vector3(rndX, transform.position.y, rndZ);

        }
        // transform.Translate(walkPoint, )
    }
}
