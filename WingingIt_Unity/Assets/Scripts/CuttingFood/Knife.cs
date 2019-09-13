using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    bool isCutting = false;
    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D coll;

    [SerializeField] GameObject trialPref;
    GameObject currentTrial;

    Vector2 prevPos;
    Vector2 newPos;

    float minVel = 0.003f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<CircleCollider2D>();
        cam = Camera.main;
    }


    void Update()
    {
        newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPos;

        if (Input.GetMouseButtonDown(0)) //Esto tendría que ser tactil
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }

        prevPos = newPos;
    }

    private void UpdateCut()
    {
        float velocity = (newPos - prevPos).magnitude * Time.deltaTime;
        if(velocity>minVel)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
        
    }

    private void StartCutting()
    {
        isCutting = true;
        coll.enabled = false;

        currentTrial = Instantiate(trialPref, this.transform);        
    }

    private void StopCutting()
    {
        isCutting = false;
        coll.enabled = false;

        currentTrial.transform.SetParent(null);
        Destroy(currentTrial,1.5f);
    }

    
}
