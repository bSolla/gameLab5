using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public float planeX, planeZ;
    int currWalkPoint;
    public float movementSpeed = 5f;
    public GameObject[] walkingPoints;
    public GameObject foodBowl;

    CharacterController charController;
    StatusMenu status;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        status = GetComponent<StatusMenu>();
        currWalkPoint = Random.Range(0, walkingPoints.Length);

        // planeX = GameObject.FindWithTag("Ground").transform.localScale.x;
        // planeZ = GameObject.FindWithTag("Ground").transform.localScale.z;

        walkingPoints = GameObject.FindGameObjectsWithTag("WalkingPoint");
        // walkPoint = Random.Range(0, walkingPoints.Length);
        // print ("Walk point: " + walkPoint);

    }

    void Update()
    {
        if(status.currState == StatusMenu.State.Normal)
        {
            movingPoint();

        }
        if(status.currState == StatusMenu.State.Hungry)
        {
            GettingFood();
        }
        
        // float dist = Vector3.Distance(transform.position, walkingPoints[walkPoint].transform.position);
        // print ("Dist " +  Mathf.RoundToInt(dist));
        // if(dist < 0.2f || dist > 3)
        // {
        //     // float rndX = Random.Range(0, planeX); print (rndX);
        //     // float rndZ = Random.Range(0, planeZ);
        //     // walkPoint = new Vector3(rndX, transform.position.y, rndZ);
        //     walkPoint = Random.Range(0, walkingPoints.Length);
        //     print ("New Walk point: " + walkPoint);
            
            
            
        // }
        // else
        // {
        //     transform.LookAt(walkingPoints[walkPoint].transform);
        //     Vector3 movement = Vector3.forward * movementSpeed * Time.deltaTime;
        //     charController.Move(movement);

        //     print ("Distance is far");
        //     // transform.Translate(walkPoint, Space.Self);
        //     // transform.position -= walkingPoints[walkPoint].transform.position * movementSpeed * Time.deltaTime;

        //     // print ("Walk point " + walkingPoints[walkPoint].transform.position);


       
        }
        // transform.Translate(walkPoint, )

    void movingPoint()
    {
        Vector3 target = walkingPoints[currWalkPoint].transform.position;
        target.y = transform.position.y;
        Vector3 moveDir = target - transform.position;
        if(moveDir.magnitude < 1f)
        {
            // transform.position = target;
            currWalkPoint = Random.Range(0, walkingPoints.Length);
        }
        else
        {
            // Vector3 lookAtTarget = Vector3.RotateTowards(transform.forward, target, 5 * Time.deltaTime, 0f);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookAtTarget), 5/Time.deltaTime);
            // transform.rotation = Quaternion.LookRotation(lookAtTarget);
            charController.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(walkingPoints[currWalkPoint].transform.position);

        }

    }
    void GettingFood()
    {
        Vector3 moveDir = foodBowl.transform.position - transform.position;
        if(moveDir.magnitude > 1)
        {
            charController.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(foodBowl.transform.position);

        }
        if(foodBowl.GetComponent<FoodBowl>().avaliableFood > 0)
        {
            status.hunger ++;
            foodBowl.GetComponent<FoodBowl>().avaliableFood --;
        }
       
    }
}
