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
    public string myName;
    public float planeX, planeZ;
    int currWalkPoint;
    public float movementSpeed = 5f;
    public GameObject[] walkingPoints;

    [HideInInspector] public Vector3 target;
    ChickenStatus status;
    PettingController petting;
    public ChickenUI chickenUI;
    public bool canMove = true, isLifted = false, isLowStatus = false;
    public int sleeping;
    public Transform perchPoint;

    float timePressed = 0;

    public bool hasHome;

    //New things
    float timeBetweenChecks=30;
    float timeNextCheck=30;

    public bool walkingToDoor;
    Transform door;
    Vector3 doorPoint;
    public Vector3 DoorPoint { get => doorPoint; set => doorPoint = value; }
    Vector3 spawnPoint;

    public string currentLocation;
    public string CurrentLocation { get => currentLocation;}

    public GameObject chickenModel;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Awake()
    {
        currentLocation = "Inside";

        chickenModel = transform.GetChild(2).gameObject;

    }

    void Start()
    {
        status = GetComponent<ChickenStatus>();

        target = newWalkingpoint();

        petting = GetComponent<PettingController>();


        float timeNextCheck=10;

        // chickenUI = this.gameObject.transform.GetChild(1).gameObject.GetComponent<ChickenUI>();

        CacheDoor();
        transform.position = new Vector3(0,0,0);

    }


    void Update()
    {
        // if(!isLifted)
        // {
            
        // }
        if (!petting.pettable || sleeping == 0 || !status.menuUI.isMenuOpen)
        {
            LiftChicken();
            if(status.currState ==ChickenStatus.ChickenState.Normal && !walkingToDoor)
            {
                StartCoroutine(movingPoint(false));
                
            }
            if (walkingToDoor)
            {
                WalkToDoor();
            }
        }
        TryForChangingLocation();

        if(Input.GetKeyDown(KeyCode.S))
        {
            sleeping = 1;
        }
        if(sleeping == 1)
        {
            GoToSleep();
        }
        // if(isLowStatus)
        // {
        //     AskingForHelp();
            
        // }

        // if(Input.GetMouseButton(1))
        // {
        //     print(door);
        //     print ("Door point: " + doorPoint);
        //     // print("Camera Rot" + Quaternion.LookRotation(Camera.main.transform.position - transform.position));
        //     // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 7f * Time.deltaTime);

        // }
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
            float randomNum = Random.Range(1, 5);                 //Put more time, depending on how much time we want the chicken to wait until move

            if (randomNum == 1)
            {
                // if (CurrentLocation == GameManager.instance.CurrentSceneName)
                // {
                    target = DoorPoint;
                    canMove = true;
                    walkingToDoor = true;
                    print("Change location -> passes the random number check -> the current location of the chicken is the same as the current scene");
                // }
            }
        }
    }


//Deactivate the Mesh and make it stop moving
    public void DeactivateChicken()
    {
        // GetComponent<MeshRenderer>().enabled = false;
        chickenModel.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        chickenUI.StopAskForHelpUI();
        canMove = false;

    }

        //Activate whatever you desactivate in the other method
    
    public void ActivateChicken()
    {
        print("Activate");
        CacheDoor();
        // target = spawnPoint;
        // gameObject.transform.position = spawnPoint;
        target = newWalkingpoint();
        transform.position = target;


        canMove = true;
        // GetComponent<MeshRenderer>().enabled = true;
        chickenModel.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = true;
        
    }
    void CacheDoor()
    {
        // print ("CacheDoor");

        door = GameObject.FindGameObjectWithTag("Door").transform;
        spawnPoint = door.GetChild(0).GetComponent<Transform>().position;
        spawnPoint.y = 0f;
        doorPoint = new Vector3(door.position.x, 0, door.position.z);
    }

    public void WalkToDoor()
    {
        if(currentLocation == GameManager.instance.CurrentSceneName)
        {
            if (Vector3.Distance(transform.position, DoorPoint) < 1.0f)
            {
                
            // walkingToDoor = false;
                // print("ello m8");
                walkingToDoor = false;
            
            
                print("Walking to door in same scene as camera");
                DeactivateChicken();
                ChickenChangesScene();
                return;

            }
            else
            {
                canMove = true;
                target = doorPoint;
                StartCoroutine(movingPoint(true));
            }
        }
        else
        {
            print("Changing scenes from the other side");
            walkingToDoor = false;

            CacheDoor();
            target = spawnPoint;
            transform.position = target;
            ChickenChangesScene();

            ActivateChicken();
            return;

            
        }

        // if (CurrentLocation == "Inside")
        // {
        //     currentLocation = "Outside";
        // }
        // else
        // {
        //     // walkingToDoor = false;
        //     currentLocation = "Inside";
        // }
            // walkingToDoor = false;

            // canMove = false;
            // return;
        // }
        // else
        // {
            // print("ello bitch");
            // canMove = true;
            // target = doorPoint;
            // StartCoroutine(movingPoint());

            // if(canMove)
            // {
            //     transform.position += doorPoint * 3 * Time.deltaTime;
            //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(doorPoint - transform.position), 7f * Time.deltaTime);     
            // }
        // }
    }

    void ChickenChangesScene()
    {
        if (CurrentLocation == "Inside")
        {
            currentLocation = "Outside";
        }
        else
        {
            // walkingToDoor = false;
            currentLocation = "Inside";
        }
    }



    public IEnumerator movingPoint(bool MoveNow)
    {
        
        Vector3 moveDir = target - transform.position;
        float targetDist = Vector3.Distance(target, transform.position);

        if(canMove && currentLocation == GameManager.instance.CurrentSceneName)
        {
            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                
                if(!MoveNow)
                {
                    canMove = false;
                    float t = Random.Range(1, 10);
                    yield return new WaitForSeconds(t);
                }
                if(!walkingToDoor  && sleeping == 0)
                {
                    target = newWalkingpoint();
                }
                canMove = true;
                // chickenUI.StopAskForHelpUI();

                
            }

            transform.position += moveDir.normalized * 2 * Time.deltaTime;
            if(sleeping < 2)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), 7f * Time.deltaTime);
                
            }
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
                // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 7f * Time.deltaTime);
                LookAtPlayer();
                if(GameManager.instance.currentSceneName == "Inside")
                {
                    if(perchPoint == null)
                    {
                        perchPoint = GameObject.FindGameObjectWithTag("Perch").transform;

                    }
                }
            }
        }

        if (isLifted && Input.GetMouseButtonUp(0))
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
        if(Vector3.Distance(transform.position, doorPoint) <= 2.5f)
        {
            walkingToDoor = true;
        }
        if(GameManager.instance.currentSceneName == "Inside")
        {
            // if(perchPoint == null)
            // {
            //     perchPoint = GameObject.FindGameObjectWithTag("Perch").transform.position;

            // }
            // // else
            // {
                if(Vector3.Distance(transform.position, perchPoint.position) <= 4f)
                {
                    sleeping = 1;

                }
            // }
            
        }
        // }
        isLifted = false;
    }

    public Vector3 newWalkingpoint()
    {
        // print("New target");
        float xPos = transform.position.x;
        float newX = Random.Range(xPos - 1, xPos + 1);

        float zPos = transform.position.z;
        float newZ = Random.Range(zPos - 2, zPos + 2);

        if(currentLocation == "Inside")
        {
            if(newX > 3.5f) newX = Random.Range(0.0f, 2.0f);
            if(newX < -5.0f) newX = Random.Range(0.0f, -2.0f);

            if(newZ > 4.8f) newZ = 4.5f;
            if(newZ < -5f) newZ = -4.5f;
        }

        if(currentLocation == "Outside")
        {
            if(newX > 2.3f) newX = 2f;
            if(newX < -4.3f) newX = -4f;

            if(newZ > 7f) newZ = -6.5f;
            if(newZ < -7f) newZ = -6.5f;
        }   
        Vector3 newTarget = new Vector3(newX, 0.1f, newZ);


        return newTarget;
    }

    public void GettingFood()
    {
        // if (GameManager.instance.CurrentSceneName=="Inside")//-------------------------------------------------should be inside - change all the strings to Inside when we change the location
        // {
        //     if (currentLocation=="Inside" || currentLocation=="Outside")
        
            // walkingToDoor = false;
            if (canMove)
            {
                Vector3 moveDir = status.Food.transform.position - transform.position;
                if (moveDir.magnitude > 1)
                {
                    transform.position += moveDir * Time.deltaTime;

                    transform.rotation = Quaternion.LookRotation(moveDir);

                }
                if (status.Food.currentAmount > 0 && Vector3.Distance(transform.position, status.Food.transform.position) < 1f)
                {
                    status.hunger++;
                    status.Food.AddAmount(-1);
                }
            }
            // else
            // {
            //     currentLocation = "Inside";
            //     ActivateChicken();
            // }
        // else
        // {
        //     // walkingToDoor = true;
            
        // }
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
                else if(status.Water.currentAmount > 0 && Vector3.Distance(transform.position, status.Water.transform.position) < 4.0f)
                {
                // print ("Here");

                    status.thirst ++;
                    status.Water.currentAmount --;
                }
       
            }
            else
            {
                currentLocation = "Inside";

            }
        }
    }
    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag != "Ground")
        {
            // print("Collided");
            // canMove = true;
            StartCoroutine(movingPoint(true));
            // return;

            // newWalkingpoint();
        }
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Perch")
        {
            print("Hit Perch");
            if(sleeping == 0)
            {
                sleeping = 1;
            }
        }
    }
    void OnCollisionStay (Collision col)
    {
        if(col.gameObject.tag == "Chicken")
        {
            print ("There's a chicken in mah face");

            target = new Vector3 (Random.Range(target.x +2, target.x -2), 0 , Random.Range(target.z +2, target.z -2));
            StartCoroutine(movingPoint(true));

            
        }
    }
    public void AskingForHelp(int AskingForWhat)     //Hungry=0 - Thirsty=1  -  Sad=2
    {
        if(GameManager.instance.CurrentSceneName == currentLocation)
        {
            Quaternion lookAtCameraQ = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
            Vector3 lookAtCameraRotation = lookAtCameraQ.eulerAngles;
            if(transform.rotation.eulerAngles == lookAtCameraRotation)
            {
                print ("Rotation is correct");
                isLowStatus = true;
                StopCoroutine(movingPoint(false));

                chickenUI.AskForHelpUI(AskingForWhat);
                StartCoroutine(DelayAskForHelp());
                

            }
            else
            {
                // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 20f * Time.deltaTime);
                // canMove = false;
                LookAtPlayer();
            }
            // target = Camera.main.gameObject.transform.position;
            
            // StartCoroutine(movingPoint());
            // isLowStatus=true;
            // isLowStatus = false;
        }
        else
        {
            chickenUI.StopAskForHelpUI();
        }


    }
    public void LookAtPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 20f * Time.deltaTime);
        if(sleeping < 1)
        {
            canMove = false;

        }
    }
    void GoToSleep()
    {
        canMove = true;
        GameObject perchTarget = GameObject.Find("PerchPoints");
        target = perchTarget.transform.position;
        movingPoint(true);
        print(target);

        if(Vector3.Distance(transform.position, target) <= 0.2f && sleeping == 1)
        {
            Vector3 newRot = new Vector3(0,0,0);
            transform.rotation = Quaternion.Euler(0,0,0);

            // isSleeping = true;
            sleeping = 2;
            StartCoroutine(Sleeping());

        }

    }
    IEnumerator Sleeping()
    {
        Animator anim = transform.GetComponentInChildren<Animator>();
        print(anim);
        GameObject.Find("SleepParticles").GetComponentInChildren<ParticleSystem>().Play();

        anim.SetBool("isSleepAnim", true);
        yield return new WaitForSeconds(10);
        anim.SetBool("isSleepAnim", false);
        GameObject.Find("SleepParticles").GetComponentInChildren<ParticleSystem>().Stop();

        yield return new WaitForSeconds(0.5f);
        // isSleeping = false;
        sleeping = 0;
        movingPoint(false);

    }
    
    public IEnumerator DelayAskForHelp()
    {
        if(isLowStatus)
        {   
            yield return new WaitForSeconds(10);
            print ("Delay, first wait");
            
            canMove=true;
            target = transform.position;
            chickenUI.StopAskForHelpUI();
            StartCoroutine(movingPoint(true));
            yield return new WaitForSeconds(10);
            print ("Delay, second wait");

            isLowStatus=false;

            


            

            // StartCoroutine(movingPoint());


        }
        

        


        // chickenController.canMove = true;
        // return;

    }

}
