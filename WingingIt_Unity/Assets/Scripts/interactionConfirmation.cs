//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                      A U T H O R  &  N O T E S
//                                    coded by Teresa, October 2019
//  controls when you can interact with interactables and makes the speechbubble pop up over the interactables
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactionConfirmation : MonoBehaviour
{

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public GameObject bubble;
    public bool confirmed = false;
    public bool uiActive = false;

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // need to make it so if you click elsewhere, the bubble disappears
        if (Input.GetMouseButtonUp(0) && (GameManager.instance.CurrentSceneName == "Outside" || GameManager.instance.CurrentSceneName == "Inside") )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider == this.gameObject.GetComponent<Collider>())
                {
                    if (!uiActive)
                    {
                        StartCoroutine (setBubbleActive());
                    }
                    if (uiActive)
                    {
                        Debug.Log ("clicked again");
                        confirmed = true;
                    }
                }
                else
                {
                    disablebubble();
                    Debug.Log ("disabling bubble idk");
                }
            } 
        }
    }

    IEnumerator setBubbleActive()
    {
        Debug.Log ("activating bubble");
        bubble.SetActive(true);
        yield return new WaitForSeconds (0.5f);
        uiActive = true;
    }
    void disablebubble()
    {
        bubble.SetActive(false);
        uiActive = false;
    }
}
