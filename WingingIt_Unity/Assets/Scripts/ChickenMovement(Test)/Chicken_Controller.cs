//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Kine and Paula, september 2019
//              controls the chicken movement in the plane and between scenes
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Controller : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    // public float planeX, planeZ;
    // int currWalkPoint;
    public float planeX, planeZ;
    int currWalkPoint;
    public float movementSpeed = 5f;
    public GameObject[] walkingPoints;

    Vector3 target;
    ChickenStatus status;
    PettingController petting;
    public bool canMove = true, isLifted = false;
    float timePressed = 0;


    //New things
    float timeBetweenChecks=30;
    float timeNextCheck=30;

    [HideInInspector]public bool walkingToDoor;
    Transform door;
    Vector3 doorPoint;
    public Vector3 DoorPoint { get => doorPoint; set => doorPoint = value; }
    Vector3 spawnPoint;

    string currentLocation;
    public string CurrentLocation { get => currentLocation;}


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Awake()
    {
        currentLocation = "Inside";
    }

    void Start()
    {
        status = GetComponent<ChickenStatus>();

        target = newWalkingpoint();

        petting = GetComponent<PettingController>();

        CacheDoor();
    }


    void Update()
    {
        if(status.currState ==ChickenStatus.State.Normal && !isLifted)
        {
            StartCoroutine(movingPoint());
            if (walkingToDoor)
            {
                WalkToDoor();
            }
        }
        if (!petting.pettable)
        {
            LiftChicken();
        }
        TryForChangingLocation();
    }


    // void ChangeLocation()
    // {
    //     if (Time.time>timeNextCheck && !isLifted)
    //     {
    //         timeNextCheck = Time.time + timeBetweenChecks;
    //         float randomNum=Random.Range(1, 2);                 //Put more time, depending on how much time we want the chicken to wait until move

    //         if (randomNum==1)
    //         {
    //             if (CurrentLocation == gm.CurrentSceneName)
    //             {
    //                 target = DoorPoint;
    //                 canMove = true;
    //                 walkingToDoor = true;
    //                 print("something");
    //             }

    //             else
    //             {
    //                 if (CurrentLocation == "Inside")
    //                 {
    //                     currentLocation = "Outside";                        
    //                 }
    //                 else
    //                 {
    //                     currentLocation = "Inside";
    //                 }
    //                 ActivateChicken();
    //             }
    //         }
    //     }
    // }

    void TryForChangingLocation()
    {
        if (Time.time > timeNextCheck && !isLifted)
        {
            timeNextCheck = Time.time + timeBetweenChecks;
            float randomNum = Random.Range(1, 2);                 //Put more time, depending on how much time we want the chicken to wait until move

            if (randomNum == 1)
            {
                if (CurrentLocation == GameManager.instance.CurrentSceneName)
                {
                    target = DoorPoint;
                    canMove = true;
                    walkingToDoor = true;
                    print("Change location -> passes the random number check -> the current location of the chicken is the same as the current scene");
                }
            }
        }
    }


//Deactivate the Mesh and make it stop moving
    public void DeactivateChicken()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        canMove = false;
    }

        //Activate whatever you desactivate in the other method
    
    public void ActivateChicken()
    {
        CacheDoor();
        gameObject.transform.position = spawnPoint;

        canMove = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }
    void CacheDoor()
    {
        door = GameObject.FindGameObjectWithTag("Door").transform;
        spawnPoint = door.GetChild(0).GetComponent<Transform>().position;
        spawnPoint.y = 0f;
        doorPoint = new Vector3(door.position.x, 0, door.position.z);
    }

    void WalkToDoor()
    {
        if (Vector3.Distance(transform.position, DoorPoint) < 0.5f)
        {
            DeactivateChicken();
            if (CurrentLocation == "Inside")
            {
                currentLocation = "Outside";
                walkingToDoor = false;
            }
            else
            {
                currentLocation = "Inside";
                walkingToDoor = false;
            }
        }      
    }



    public IEnumerator movingPoint()
    {
        
        Vector3 moveDir = target - transform.position;

        if(canMove)
        {
            if(Vector3.Distance(transform.position, target) < 0.1f)
            {

                canMove = false;
                float t = Random.Range(1, 10);
                yield return new WaitForSeconds(t);
                target = newWalkingpoint();
                canMove = true;
            }

            transform.position += moveDir * 3 * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), 7f * Time.deltaTime);

        }

    }


    void LiftChicken()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == this.gameObject.GetComponent<Collider>()&&!isLifted)
                {
                    timePressed += Time.deltaTime;

                    if (timePressed > 1)
                    {
                        isLifted = true;

                    }
                }
            }

            if (isLifted)
            {
                Vector3 moveDir = new Vector3(hit.point.x, 0.5f, hit.point.z);
                // Vector3 moveDir = hit.point;

                transform.position = moveDir;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 7f * Time.deltaTime);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(DelayLiftChicken());
            timePressed = 0;
            target= new Vector3(transform.position.x, 0.1f, transform.position.z);

            transform.position = target;
        }
    }
    //This is a delay until the end of the frame so the menu doesn't open when you stop lifting the chicken
    IEnumerator DelayLiftChicken()
    {
        yield return new WaitForEndOfFrame();
        isLifted = false;
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

        if(GameManager.instance.CurrentSceneName == "Inside")
        {
            if(newX > 4.8f)
            {
                newX = 4;
            }
            if(newX < -4.8f)
            {
                newX = -4;
            }
            if(newZ > 3.4f)
            {
                newZ = 3;
            }
            if(newZ < -5)
            {
                newZ = -4.5f;
            }
        }
        if(GameManager.instance.CurrentSceneName == "Outside")
        {
            if(newX > 2.3f)
            {
                newX = 2;
            }
            if(newX < -4.3f)
            {
                newX = -4;
            }
            if(newZ > 7f)
            {
                newZ = 6.5f;
            }
            if(newZ < -7)
            {
                newZ = -6.5f;
            }
        }    
        Vector3 newTarget = new Vector3(newX, 0.1f, newZ);


        return newTarget;
    }

    public void GettingFood()
    {
        if (GameManager.instance.CurrentSceneName=="Inside")//-------------------------------------------------should be inside - change all the strings to Inside when we change the location
        {
            if (currentLocation=="Inside")
            {
                Vector3 moveDir = status.Food.transform.position - transform.position;
                if (moveDir.magnitude > 1)
                {
                    transform.position += moveDir * Time.deltaTime;

                    transform.rotation = Quaternion.LookRotation(status.Food.transform.position);

                }
                if (status.Food.avaliableFood > 0 && Vector3.Distance(transform.position, status.Food.transform.position) < 1f)
                {
                    status.hunger++;
                    status.Food.AddFood(-1);
                }
            }
            else
            {
                currentLocation = "Inside";
                ActivateChicken();
            }
        }
        else
        {
            walkingToDoor = true;
        }
    }
    public void GettingWater()
    {

        if (GameManager.instance.CurrentSceneName=="Inside")//-------------------------------------------------should be inside - change all the strings to Inside when we change the location
        {
            if (currentLocation=="Inside")
            {
                Vector3 moveDir = status.Water.transform.position - transform.position;
                if(moveDir.magnitude > 1)
                {
                    transform.position += moveDir * 2 * Time.deltaTime;

                    transform.rotation = Quaternion.LookRotation(status.Water.transform.position);

                }
                else if(status.Water.waterAvaliable > 0 && Vector3.Distance(transform.position, status.Water.transform.position) < 4.0f)
                {
                print ("Here");

                    status.thirst ++;
                    status.Water.waterAvaliable --;
                }
       
            }
            else
            {
                currentLocation = "Inside";

            }
        }
    }

}
