// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      A U T H O R & N O T E S
//                                                coded by: Kine - September 2019
//                                  When you click on the egg, it gives some feedback before disapearing.
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPickUp : MonoBehaviour
{
// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public Collider eggCol;
    bool hasClicked = false;

// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                                      F U N C T I O N S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Update()
    {
        if(eggCol != null)
        {
            StartCoroutine(PickUpEgg());
        }
        
        if(hasClicked && eggCol == null)
        {
            hasClicked = false;
        }
    }

    // An Ienumerator that gives player feedback for the pickup before destroying the egg.
    private IEnumerator PickUpEgg()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // EggCol is given by the EggDrop script.
        if(eggCol != null && Physics.Raycast(ray, out hit, 100) && !hasClicked)   
        {
            // eggCol = GameObject.FindGameObjectWithTag("Egg").GetComponentInChildren<Collider>();;
            if(hit.collider == eggCol && Input.GetMouseButtonDown(0))
            {
                // print("I touch the egg");
                hasClicked = true;
                eggCol.gameObject.transform.localScale*=2;
                eggCol.gameObject.transform.position = new Vector3(0, 5, 0);
                yield return new WaitForSeconds(1);
                Destroy(eggCol.gameObject);

            }
        }
    }
}
