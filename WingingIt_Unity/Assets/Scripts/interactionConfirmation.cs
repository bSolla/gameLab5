using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactionConfirmation : MonoBehaviour
{
    public GameObject bubble;
    public bool confirmed = false;
    public bool uiActive = false;

    // Start is called before the first frame update
    void Start()
    {
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // need to make it so if you click elsewhere, the bubble disappears
        if (Input.GetMouseButtonUp(0))
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
