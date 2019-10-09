using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float transitionDuration = 2.5f;
    public Transform CoopTarget, StoreTarget, InsideCoopTarget, InsideStoreTarget;
    public Vector3 Coop, Store, InsideCoop, InsideStore;
    public bool moveCam, inside;
    public int whichTarget;
    // Start is called before the first frame update
    void Start()
    {
        Coop = new Vector3 (CoopTarget.position.x, CoopTarget.position.y, -4);
        Store = new Vector3 (StoreTarget.position.x, StoreTarget.position.y, -4);
        InsideCoop = new Vector3 (InsideCoopTarget.position.x, InsideCoopTarget.position.y, 4);
        InsideStore = new Vector3 (InsideStoreTarget.position.x, InsideStoreTarget.position.y, 4);
        transform.position = Coop;
        inside = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp (KeyCode.C)) 
        {
            StartCoroutine (Transition());
        }
        
        if (Input.GetKeyUp (KeyCode.V))
        {
            // Debug.Log ("key v");
            StartCoroutine (zoomTransition());
        }

        if (transform.position == Coop)
        {
            whichTarget = 0;
        }

        if (transform.position == Store)
        {
            whichTarget = 1;
        }
    }

    IEnumerator Transition ()
    {
        float t = 0.0f;
        Vector3 startPos = transform.position;
        
            if (whichTarget == 0 && !inside)
            {
                while (t < 1.0f)
                { 
                    // Debug.Log ("moving cam to store");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, Store, t);
                    yield return 0;
                }
            }

           else if (whichTarget == 1 && !inside)
            {
                while (t < 1.0f)
                { 
                    // Debug.Log ("moving cam to coop");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, Coop, t);
                    yield return 0;
                }
            }
            
        }
    

    IEnumerator zoomTransition ()
    {
        float t = 0.0f;
        Vector3 startPos = transform.position;
        if (whichTarget == 0 && !inside)
            {
                while (t < 1.0f)
                {
                    Debug.Log ("moving cam inside coop");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, InsideCoop, t);
                    inside = true;
                    //StartCoroutine (insideBool ());
                    yield return 0;
                }
                
                
            }
            else if (whichTarget == 0 && inside)
            {
                while (t < 1.0f)
                {
                    Debug.Log ("moving cam outside coop");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, Coop, t);
                    inside = false;
                    yield return 0;
                }
            }
        
        if (whichTarget == 1 && !inside)
            {
                while (t < 1.0f)
                {
                    Debug.Log ("moving cam inside store");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, InsideStore, t);
                    inside = true;
                    yield return 0;
                }
            }
            else if (whichTarget == 1 && inside)
            {
                while (t < 1.0f)
                {
                    Debug.Log ("moving cam outside store");
                    t += Time.deltaTime * (Time.timeScale/transitionDuration);
                    transform.position = Vector3.Lerp (startPos, Store, t);
                    inside = false;
                    yield return 0;
                }
            }

        }

    IEnumerator insideBool ()
    {
        yield return new WaitForSeconds (1);
        inside = true;

    }

}
