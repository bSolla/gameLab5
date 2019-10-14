using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float transitionDuration;
    // public Transform CoopTarget, InsideCoopTarget;
    public Transform door;
    Vector3 Coop;
    float originalOrthographicSize;
    public bool moveCam;
    public int whichTarget;
    // Start is called before the first frame update
    public string currentScene;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        print("Start Camera");
        transitionDuration=1.5f;
        // Coop = new Vector3 (CoopTarget.position.x, CoopTarget.position.y, -4);
        // // Store = new Vector3 (StoreTarget.position.x, StoreTarget.position.y, -4);
        // InsideCoop = new Vector3 (InsideCoopTarget.position.x, InsideCoopTarget.position.y, 4);
        // // InsideStore = new Vector3 (InsideStoreTarget.position.x, InsideStoreTarget.position.y, 4);
        // transform.position = Coop;

        originalOrthographicSize = cam.orthographicSize;

        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyUp (KeyCode.C)) 
        // {
        //     StartCoroutine (Transition());
        // }
        
        if (Input.GetKeyUp (KeyCode.V))
        {
            // Debug.Log ("key v");
            StartCoroutine (zoomTransition());
        }

        if (transform.position == Coop)
        {
            whichTarget = 0;
        }

    }
    public void startZoom()
    {
        StartCoroutine (zoomTransition());

    }


    IEnumerator zoomTransition ()
    {
        float t = 0.0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        // if (currentScene == "Inside" || currentScene == "Outside")
        // {
            while (t < 1.0f)
            {
                Debug.Log ("moving cam inside coop");
                t += Time.deltaTime * (Time.timeScale/transitionDuration);
                // transform.position = Vector3.Lerp (startPos, door.position * 0.1f, t);
                

                // elapsed += Time.deltaTime / duration;
                cam.orthographicSize = Mathf.Lerp (originalOrthographicSize, originalOrthographicSize- 2f, t);
                transform.rotation = Quaternion.Lerp (startRot, Quaternion.LookRotation(door.position * 0.5f - transform.position), t);

                // transform.rotation = Quaternion.Lerp (originRotation, newRotation, elapsed);
                // inside = true;
                //StartCoroutine (insideBool ());
                yield return 0;
            }
            
            
        // }

    }


}
