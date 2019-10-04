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

    public float planeX, planeZ;
    int currWalkPoint;
    public float movementSpeed = 5f;
    public GameObject[] walkingPoints;

    Vector3 target;
    ChickenStatus chickenStatus;
    PettingController petting;
    public bool canMove = true, isLifted = false;
    float timePressed = 0;


    //New things
    float timeBetweenChecks=60;
    float timeNextCheck=60;

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
        currentLocation = "Outside";
    }


    void Start()
    {
        chickenStatus = GetComponent<ChickenStatus>();

        target = newWalkingpoint();

        petting = GetComponent<PettingController>();

        CacheDoor();
    }


    void Update()
    {
        if(chickenStatus.currState ==ChickenStatus.State.Normal && !isLifted)
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
        TryForChangeLocation();
    }


    void TryForChangeLocation()
    {
        if (Time.time>timeNextCheck && !isLifted)
        {
            timeNextCheck = Time.time + timeBetweenChecks;
            float randomNum=Random.Range(1, 2);                 //Put more time, depending on how much time we want the chicken to wait until move

            if (randomNum==1)
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


    public void DeactivateChicken()
    {
        //Deactivate the Mesh and make it stop moving
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        canMove = false;
    }

    public void ActivateChicken()
    {
        //Activate whatever you deactivate in the other method
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
        if (Vector3.Distance(transform.position, doorPoint) < 1f)
        {
            DeactivateChicken();
            if (CurrentLocation == "Inside")
            {
                currentLocation = "Outside";
                walkingToDoor = false;
                Debug.Log("changed chicken location to outside");
            }
            else
            {
                currentLocation = "Inside";
                walkingToDoor = false;
                Debug.Log("changed chicken location to inside");
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
                if (!isLifted && hit.collider == this.gameObject.GetComponent<Collider>())
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

        Vector3 newTarget = new Vector3(newX, 0.1f, newZ);

        return newTarget;
    }

    public void GettingFood()
    {
        if (GameManager.instance.CurrentSceneName=="Outside")//-------------------------------------------------should be inside - change all the strings to Inside when we change the location
        {
            if (currentLocation=="Outside")
            {
                Vector3 moveDir = chickenStatus.Food.transform.position - transform.position;
                if (moveDir.magnitude > 1)
                {
                    transform.position += moveDir * Time.deltaTime;

                    transform.rotation = Quaternion.LookRotation(chickenStatus.Food.transform.position);

                }
                if (chickenStatus.Food.avaliableFood > 0 && Vector3.Distance(transform.position, chickenStatus.Food.transform.position) < 1f)
                {
                    chickenStatus.hunger++;
                    chickenStatus.Food.avaliableFood--;
                }
            }
            else
            {
                currentLocation = "Outside";
                ActivateChicken();
            }
        }
        else
        {
            walkingToDoor = true;
        }
    }
}
