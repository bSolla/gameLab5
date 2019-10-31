//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                    A U T H O R  &  N O T E S
//                                   coded by Paula, october 2019
//   When is clicked, this button change the texture in scene if the object is there, and store the new texture in the StoreColors script
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public LevelReguards.Reguard rg;
    public bool isInScene=false;
    public Material modelMat;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeTexture); 
    }


    void ChangeTexture()
    {
        if (isInScene)
        {
            if (rg.type== LevelReguards.TextureType.Fences)
            {
                MeshRenderer[] mr= GameObject.Find("Fences").GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer item in mr)
                {
                    if (item.material.name != "FencePlaneTileTexture (Instance)")
                    { item.material.mainTexture = rg.tx; }                    
                }
            }
            else
            { modelMat.mainTexture = rg.tx; }
            
        }

        FindObjectOfType<StoreColors>().StoreNewTexture(rg);
    }
}
