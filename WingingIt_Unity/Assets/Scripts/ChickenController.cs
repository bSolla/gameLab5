using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    StatusMenu statusMenu;
    public float planeX, planeZ;
    int walkPoint;
    public float movementSpeed = 5f;
    public GameObject[] walkingPoints;

    void Start()
    {
        planeX = GameObject.FindWithTag("Ground").transform.localScale.x;
        planeZ = GameObject.FindWithTag("Ground").transform.localScale.z;

        walkingPoints = GameObject.FindGameObjectsWithTag("WalkingPoint");
        walkPoint = Random.Range(0, walkingPoints.Length);
    }

    void Update()
    {
        
        print ("Walk point: " + walkPoint);
        if(Vector3.Distance(transform.position, walkingPoints[walkPoint].transform.position) > 0.1f)
        {
            print ("Distance is far");
            // transform.Translate(walkPoint, Space.Self);
            transform.position += walkingPoints[walkPoint].transform.position * movementSpeed * Time.deltaTime;
            
        }
        else
        {
            // float rndX = Random.Range(0, planeX); print (rndX);
            // float rndZ = Random.Range(0, planeZ);
            // walkPoint = new Vector3(rndX, transform.position.y, rndZ);
            walkPoint = Random.Range(0, walkingPoints.Length);

       
        }
        // transform.Translate(walkPoint, )
    }
}
