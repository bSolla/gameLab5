using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryMinigame : MonoBehaviour
{
    private bool mousePressed = false;
    private int currentBerry = 0;
    private int totalBerryCount = 0;

    BerryBehavior[] berryArray;
    [SerializeField] GameObject trailPrefab;
    GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        berryArray = gameObject.GetComponentsInChildren<BerryBehavior>();
        totalBerryCount = berryArray.Length;

        trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            currentBerry = 0;

            Destroy(trail);
            trail = Instantiate(trailPrefab, berryArray[0].transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (mousePressed)
        {
            Debug.Log("mouse pressed");
            if (currentBerry < totalBerryCount && berryArray[currentBerry].beingHoveredByMouse)
            {
                trail.transform.position = berryArray[currentBerry].transform.position;
                currentBerry++;
            }
        }
    }
}
