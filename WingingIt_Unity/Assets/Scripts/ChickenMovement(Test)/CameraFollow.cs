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
    public float duration = 1f;
    public float elapsed = 0f;
    public bool transitioning = false;
    // Start is called before the first frame update
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
        if (transitioning)
        {
            if (following)
            {
                Debug.Log ("target: " + target);
                followChicken();
                elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (7.91f, 2f, elapsed);
            }
            else
            {
                elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (2, 7.91f, elapsed);
            }
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
        transform.position = originPosition;
        transform.rotation = originRotation;

    }
}
