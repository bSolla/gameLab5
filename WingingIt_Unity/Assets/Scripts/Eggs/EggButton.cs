using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CallOtherMethod);
    }

    //I need to do this because with the add listener i cant pass a reference of the material to the other script
    void CallOtherMethod()
    {
        EggColection col=FindObjectOfType<EggColection>();
        if (col.showingEgg)
        {
            col.bigEgg.SetActive(false);
            col.showingEgg = false;
        }
        else { col.ShowEgg(GetComponentInChildren<MeshRenderer>().material); }
        
    }
}
