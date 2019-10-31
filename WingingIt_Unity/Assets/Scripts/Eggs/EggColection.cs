using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggColection : MonoBehaviour
{
    [SerializeField] GameObject colectionPanel;
    EggManager em;

    [SerializeField] GameObject eggButton;

    public GameObject bigEgg;

    Transform subpanel;

    public bool showingEgg;
    float timeHolding;

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EggManager>();
        colectionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (colectionPanel.activeSelf)
        {
            if (showingEgg)
            {
                RotateEgg();
                HideEgg();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OpenPanel();
        }
    }

    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<       C R E A T E   T H E   P I C T U R E S       >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public void GenerateCommonPictures()
    {
        subpanel = colectionPanel.transform.Find("Common Eggs Panel");
        subpanel.gameObject.SetActive(true);

        foreach (EggInfo egg in em.CommonEggs)
        {
            GameObject newButton = Instantiate(eggButton, subpanel);
            if (egg.owned)
            {
                newButton.GetComponentInChildren<MeshRenderer>().material = egg.eggMaterial;
            }
        }
    }

    public void GenerateRarePictures()
    {
        subpanel = colectionPanel.transform.Find("Rare Eggs Panel");
        subpanel.gameObject.SetActive(true);

        foreach (EggInfo egg in em.RareEggs)
        {
            GameObject newButton = Instantiate(eggButton, subpanel);
            if (egg.owned)
            {
                newButton.GetComponentInChildren<MeshRenderer>().material = egg.eggMaterial;
            }
        }
    }

    public void GenerateLegendaryPictures()
    {
        subpanel=colectionPanel.transform.Find("Legendary Eggs Panel");
        subpanel.gameObject.SetActive(true);

        foreach (EggInfo egg in em.LegendaryEggs)
        {
            GameObject newButton = Instantiate(eggButton, subpanel);
            if (egg.owned)
            {
                newButton.GetComponentInChildren<MeshRenderer>().material = egg.eggMaterial;
            }
        }
    }

    public void DestroyPictures()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Egg Button");
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

        subpanel.gameObject.SetActive(false);
    }


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<        O P E N   A N D   C L O S E   M E T H O D S         >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void OpenPanel()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider == this.GetComponent<BoxCollider>())
            {
                colectionPanel.SetActive(true);
                //GeneratePictures();
            }
        }
    }

    public void ClosePanel()
    {
        colectionPanel.SetActive(false);
    }


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<          S H O W   T H E   B I G   E G G   M E T H O D S          >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Maybe make the other buttons cannot be clicked while the big egg is active

    public void ShowEgg(Material mat)
    {
        bigEgg.SetActive(true);
        bigEgg.GetComponent<MeshRenderer>().material = mat;
        StartCoroutine(DelayBool());
    }

    IEnumerator DelayBool()
    {
        yield return new WaitForSeconds(0.1f);
        showingEgg = true;

    }

    void HideEgg()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            if (timeHolding < 0.8f)
            {
                bigEgg.SetActive(false);
                showingEgg = false;
            }            
            timeHolding = 0;
        }
    }

    void RotateEgg()
    {
        if (Input.GetMouseButton(0))
        {
            timeHolding += Time.deltaTime;
            float xAxis = Input.GetAxis("Mouse X");
            bigEgg.transform.Rotate(Vector3.up*xAxis*Time.deltaTime);
        }
    }
}



//Now the collection is created and destroyed each time we open and close the panel, there is more efficient ways to do this, but maybe later