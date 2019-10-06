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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log (following);
        if (transitioning)
        {
            if (following)
            {
                Debug.Log ("target: " + target);
                followChicken();
                elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (7.91f, 2f, elapsed);
                transform.rotation = Quaternion.Lerp (originRotation, newRotation, elapsed);
            }
            else
            {
                elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (2, 7.91f, elapsed);
                transform.rotation = Quaternion.Lerp (transform.rotation, originRotation, elapsed);
            }
        }
        else
        {
            return;
        }   
    }

    public void followChicken ()
    {
        Debug.Log ("following that chicken over there aww yeah");

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
        //cam.orthographicSize = 7.91f;
        //transform.position = originPosition;
        //transform.rotation = originRotation;

    }
}
