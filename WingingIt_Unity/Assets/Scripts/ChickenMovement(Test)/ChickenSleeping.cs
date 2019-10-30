using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSleeping : MonoBehaviour
{
    Chicken_Controller chicken_Controller;
    Animator anim;
    ParticleSystem particles;
    public Transform perchTarget;
    public int sleeping;
    [HideInInspector] public bool isSleeping;


    void Start()
    {
        chicken_Controller = GetComponent<Chicken_Controller>();
        perchTarget = GameObject.Find("PerchPoints").transform;
        anim = transform.GetComponentInChildren<Animator>();

        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            sleeping = 1;
        }
        if(sleeping == 1)
        {
            GoToSleep();
        }
        
    }
    // public void LookForPerch()
    // {
    //     if()
    // }

    void GoToSleep()
    {
        if(chicken_Controller.currentLocation == GameManager.instance.CurrentSceneName)
        {
            chicken_Controller.canMove = false;
            isSleeping = true;
            // chicken_Controller.target = perchTarget.transform.position;
            // chicken_Controller.StartCoroutine(movingPoint(true));
            // print(target);

            if(Vector3.Distance(transform.position, perchTarget.transform.position) <= 0.2f && sleeping == 1)
            {
                Vector3 newRot = new Vector3(0,0,0);
                transform.rotation = Quaternion.Euler(0,0,0);

                // isSleeping = true;
                sleeping = 2;
                StartCoroutine(Sleeping());

            }
            else
            {
                Vector3 moveDir = perchTarget.position - transform.position;
                transform.position += moveDir.normalized * 2 * Time.deltaTime;
                // if(!sleeping.isSleeping)
                // {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(perchTarget.position - transform.position), 7f * Time.deltaTime);
                    
                // }

                
            }
        }

    }
    IEnumerator Sleeping()
    {
        
        // print(anim);
        particles.Play();

        anim.SetBool("isSleepAnim", true);
        yield return new WaitForSeconds(10);
        anim.SetBool("isSleepAnim", false);
        particles.Stop();

        yield return new WaitForSeconds(2.0f);
        // isSleeping = false;
        sleeping = 0;
        // movingPoint(false);
        chicken_Controller.canMove = true;
        isSleeping = false;


    }
    void OnTriggerEnter(Collider col)
    {
        if(chicken_Controller.isLifted)
        {
            if(col.gameObject.tag == "Perch")
            {
                // print("Hit Perch");
                perchTarget = col.transform.GetChild(2).transform;
                particles = GameObject.Find("SleepParticles").GetComponentInChildren<ParticleSystem>();


                if(sleeping == 0)
                {
                    sleeping = 1;
                }
            }
        }
        
    }
}
