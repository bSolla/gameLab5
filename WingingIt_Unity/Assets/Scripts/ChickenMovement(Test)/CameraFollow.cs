using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 originPosition;
    public Quaternion originRotation;
    //public Vector3 targetToFollow;
    private float smooth = 5f;
    public bool following = false;
    public Transform target;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        following = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            Debug.Log ("target: " + target);
            followChicken();
        }
        else
        {

        }
        
    }

    public void followChicken ()
    {
        Debug.Log ("following that chicken over there aww yeah");

        //transform.position = Vector3.Lerp (transform.position, targetToFollow, Time.deltaTime * smooth);
        transform.LookAt(target);
    }

    public void startFollowing(Transform targetToFollow)
    {
        following = true;
        cam.orthographicSize = 2;
        target = targetToFollow;
    }

    public void stopFollowing()
    {
        following = false;
        cam.orthographicSize = 7.91f;
        transform.position = originPosition;
        transform.rotation = originRotation;

    }
}
