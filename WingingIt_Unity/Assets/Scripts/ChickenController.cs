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
    public Transform rotatePoint;

    Vector3 target;
    CharacterController charController;
    StatusMenu status;
    bool canMove = true;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        status = GetComponent<StatusMenu>();
        currWalkPoint = Random.Range(0, walkingPoints.Length);

        // planeX = GameObject.FindWithTag("Ground").transform.localScale.x;
        // planeZ = GameObject.FindWithTag("Ground").transform.localScale.z;

        walkingPoints = GameObject.FindGameObjectsWithTag("WalkingPoint");
        target = newWalkingpoint();
        rotatePoint.transform.position = target;

        // walkPoint = Random.Range(0, walkingPoints.Length);
        // print ("Walk point: " + walkPoint);

    }

    void Update()
    {
        if(status.currState == StatusMenu.State.Normal)
        {
            StartCoroutine(movingPoint());
            RotationPointMovement();


        }
        // if(status.currState == StatusMenu.State.Hungry)
        // {
        //     GettingFood();
        // }
        
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

    // public void movingPoint()
    // {
    //     Vector3 target = walkingPoints[currWalkPoint].transform.position;
    //     target.y = transform.position.y;
    //     Vector3 moveDir = target - transform.position;
    //     if(moveDir.magnitude < 1f)
    //     {
    //         // transform.position = target;
    //         currWalkPoint = Random.Range(0, walkingPoints.Length);
    //     }
    //     else
    //     {
    //         // Vector3 lookAtTarget = Vector3.RotateTowards(transform.forward, target, 5 * Time.deltaTime, 0f);
    //         // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookAtTarget), 5/Time.deltaTime);
    //         // transform.rotation = Quaternion.LookRotation(lookAtTarget);
    //         charController.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
    //         transform.rotation = Quaternion.LookRotation(walkingPoints[currWalkPoint].transform.position);

    //     }

    // }
    public IEnumerator movingPoint()
    {
        // Vector3 target = newWalkingpoint();
        // target.y = transform.position.y;
        Vector3 moveDir = target - transform.position;
        // if(moveDir.magnitude < 1f)
        // print("Distance " + Vector3.Distance(transform.position, target));
        // if(Vector3.Distance(transform.position, target) < 0.1f && canMove)
        // {
        //     // transform.position = target;
        //     // currWalkPoint = Random.Range(0, walkingPoints.Length);
        //     canMove = false;
        //     yield return new WaitForSeconds(2);
        //     target = newWalkingpoint();
        //     canMove = true;
        //     // newWalkingpoint();
        // }
        if(canMove)
        {
            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                // transform.position = target;
                // currWalkPoint = Random.Range(0, walkingPoints.Length);
                canMove = false;
                yield return new WaitForSeconds(2);
                target = newWalkingpoint();
                canMove = true;
                // newWalkingpoint();
            }
            // Vector3 lookAtTarget = Vector3.RotateTowards(transform.forward, target, 5 * Time.deltaTime, 0f);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookAtTarget), 5/Time.deltaTime);
            // transform.rotation = Quaternion.LookRotation(lookAtTarget);
            charController.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
            transform.LookAt(rotatePoint);

        }

    }
    public Vector3 newWalkingpoint()
    {
        // print("New target");
        float xPos = transform.position.x;
        float newX = Random.Range(xPos - 1, xPos + 1);
        if(newX > 2) newX = Random.Range(0.0f, 2.0f);
        if(newX < -2) newX = Random.Range(0.0f, -2.0f);

        float zPos = transform.position.z;
        float newZ = Random.Range(zPos - 2, zPos + 2);
        if(newZ > 2) newZ = 2;
        if(newZ < -2) newZ = -2;

        Vector3 newTarget = new Vector3(newX, transform.position.y, newZ);

        print ("The new target is: " + newTarget);
        return newTarget;
    }
    public void GettingFood()
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

    private void RotationPointMovement()
    {
        float offsetX = rotatePoint.rotation.x - transform.rotation.x;
        float offsetZ = rotatePoint.position.z - transform.position.z;
        Vector3 moveDir = (target) - rotatePoint.position;
        rotatePoint.transform.position = new Vector3(rotatePoint.transform.position.x + offsetX, 0.35f, rotatePoint.transform.position.z + offsetZ);
        rotatePoint.position += moveDir * 3 * Time.deltaTime; 
    }
}
