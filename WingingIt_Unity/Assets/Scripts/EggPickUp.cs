using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPickUp : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(PickUpEgg());
    }

    private IEnumerator PickUpEgg()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            Collider eggCol = GameObject.FindGameObjectWithTag("Egg").GetComponentInChildren<Collider>();;
            if(hit.collider == eggCol && Input.GetMouseButtonDown(0))
            {
                print("I touch the egg");
                eggCol.gameObject.transform.localScale*=2;
                eggCol.gameObject.transform.position = new Vector3(0, 5, 0);
                yield return new WaitForSeconds(1);
                Destroy(eggCol.gameObject);

            }
        }
    }
}
