using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryGameManager : MonoBehaviour
{
    [SerializeField] GameObject[] berryGroup;
    GameObject[] berry;
    [SerializeField] GameObject trailPref;
    GameObject trail;
    int arrayPos = 0;
    bool lastPos=false;

    // Start is called before the first frame update
    void Start()
    {
        int num=Random.Range(0,berryGroup.Length); //This is for choosing the levels randomly, putting them all in the scene inactive, if we want a level selection we need to change this
        berryGroup[num].SetActive(true);

        berry=GameObject.FindGameObjectsWithTag("Berry");
        trail=Instantiate(trailPref, berry[0].transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BerryClick(GameObject thatBerry)
    {
        thatBerry.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        StartCoroutine(BerryScale(thatBerry));

        if (thatBerry==berry[arrayPos])
        {
            trail.transform.position = berry[arrayPos].transform.position;
            arrayPos++;
            if (lastPos)
            {
                EndMiniGame();
            }
            if (arrayPos == berry.Length)
            {
                arrayPos = 0;
                lastPos = true;
            }           
        }
    }

    IEnumerator BerryScale(GameObject thisBerry)
    {
        yield return new WaitForSeconds(0.1f);
        thisBerry.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

    }

    public void EndMiniGame() //------------------------NEED TO BE DONE-----------------------------
    {
        print("congrats!!");
    }
}
