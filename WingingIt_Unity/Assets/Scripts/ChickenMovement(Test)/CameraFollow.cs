//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Teresa, October 2019
//              zooms in when you clicke the chicken and follows its movement
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public Vector3 originPosition;
    public Quaternion originRotation, newRotation;
    float originalOrthographicSize;
    //public Vector3 targetToFollow;
    private float smooth = 5f;
    public bool following = false;
    public Transform target;
    Camera cam;
    public float duration = 1f;
    public float elapsed = 0f;
    public bool transitioning = false;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        following = false;
        cam = Camera.main;
        transitioning = false;
        originalOrthographicSize = cam.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        if (transitioning)
        {
            if (following)
            {
                // Debug.Log ("target: " + target);
                followChicken();
                if(cam.orthographicSize != 2)
                {
                    elapsed += Time.deltaTime / duration;
                    cam.orthographicSize = Mathf.Lerp (originalOrthographicSize, 2f, elapsed);
                    transform.rotation = Quaternion.Lerp (originRotation, newRotation, elapsed);
                }
            }
            else
            {
                elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (2, originalOrthographicSize, elapsed);
                transform.rotation = Quaternion.Lerp (transform.rotation, originRotation, elapsed);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            print(cam);
        }
   
    }

    public void followChicken ()
    {
        // Debug.Log ("following that chicken over there aww yeah");

        //transform.position = Vector3.Lerp (transform.position, targetToFollow, Time.deltaTime * smooth);
        transform.LookAt(target);
        newRotation = transform.rotation;
    }

    public void startFollowing(Transform targetToFollow)
    {
        following = true;
        transitioning = true;
        elapsed = 0;
        //cam.orthographicSize = 2;
        target = targetToFollow;
    }

    public void stopFollowing()
    {
        elapsed = 0;
        following = false;
    }
}
