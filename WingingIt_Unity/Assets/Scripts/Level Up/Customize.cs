//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                    A U T H O R  &  N O T E S
//                                   coded by Paula, october 2019
//               Paint the textures stored and creates the custom buttons 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    StoreColors sColors;
    GameManager gm;
    LevelReguards lr;
    LevelManager lm;

    
    [SerializeField] GameObject buttonPref;

    //References to the models   --------------------------Add new things
    Material coopMat;
    Material doorMat;
    Material[] fencesMat;
    Material insideMat;
    Material nestMat;
    Material perchMat;



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Probably this should be in the gm, but I don't want to touch it because i feel there would be problems while merging
    void Start()
    {
        if (gm==null)
        {
            gm = FindObjectOfType<GameManager>();
            UnityEditor.Selection.activeObject = gm.gameObject;
        }

        gm = GameManager.instance;

        lm = gm.GetComponent<LevelManager>();
        sColors = gm.GetComponent<StoreColors>();
        lr = gm.GetComponent<LevelReguards>();

               

        
        //Search for the models in the current scene --------------------------Add new things
        if (gm.CurrentSceneName == "Inside")
        {
            insideMat = GameObject.Find("CoopInsideModel").GetComponentInChildren<MeshRenderer>().material;
            nestMat = GameObject.Find("NestHub").GetComponentInChildren<MeshRenderer>().material;
            perchMat = GameObject.Find("Perch").GetComponentInChildren<MeshRenderer>().material;
            fencesMat = new Material[1];
        }
        else if (gm.CurrentSceneName == "Outside")
        {
            coopMat = GameObject.Find("CoopModel").GetComponentInChildren<MeshRenderer>().material;
            doorMat = GameObject.Find("Door/DoorFlap").GetComponentInChildren<MeshRenderer>().material;
            MeshRenderer[] fences = GameObject.Find("Fences").GetComponentsInChildren<MeshRenderer>();

            fencesMat = new Material[fences.Length-21];

            int i = 0;
            foreach (MeshRenderer mesh in fences)
            {
                string matName = mesh.material.name;
                if (matName != "FencePlaneTileTexture (Instance)")
                {                    
                    fencesMat[i] = mesh.material;
                    i++;
                }
            }
        }



        StartCoroutine(PaintTextures()); //--------------------------------I have to do this early somewhere because when the scene begins we can se how the color change
    }


    //When the scene is loaded it changes all the materials to the texture that is stored, so the customization is still there when you change scenes
    IEnumerator PaintTextures()
    {
        yield return new WaitForEndOfFrame(); 

        if (gm.CurrentSceneName == "Outside")
        {
            coopMat.mainTexture= sColors.CoopTexture;
            doorMat.mainTexture = sColors.DoorTexture;
            foreach (Material item in fencesMat)
            {
                item.mainTexture = sColors.FencesTexture;
            }
        }

        else if (gm.CurrentSceneName == "Inside")
        {
            insideMat.mainTexture = sColors.InsideTexture;
            nestMat.mainTexture = sColors.NestTexture;
            perchMat.mainTexture = sColors.PerchTexture;
        }
    }


    //Depending on with button call this method it instantiates buttons with all the textures of one type, the string name is wrotten in the inspector
    public void InstantiateTexturesButtons(string name)
    {
        GameObject panel=GameObject.Find("TexturesPanel");

        //Destroy previous buttons
        Button[] children = panel.GetComponentsInChildren<Button>();
        foreach (Button item in children)
        {
            Destroy(item.transform.gameObject);
        }

        int i=0;
        string sceneName="";
        Material mat=null;

        //In this switch it assign the name of the scene in which the object is, the material the button has to change and the position in the array
        switch (name)
        {
            case "Coop":
                i = 0;
                sceneName = "Outside";
                mat = coopMat;
                break;

            case "Door":
                i = 1;
                sceneName = "Outside";
                mat = doorMat;
                break;

            case "Fences":
                i = 2;
                sceneName = "Outside";
                mat = fencesMat[0];
                break;

            case "Inside":
                i = 3;
                sceneName = "Inside";
                mat = insideMat;
                break;

            case "Nest":
                i = 4;
                sceneName = "Inside";
                mat = nestMat;
                break;

            case "Perch":
                i = 5;
                sceneName = "Inside";
                mat = perchMat;
                break;

            default:
                break;
        }  //A case for each customizable thing indicating the position in the array and the scene in where they are


        int j = 0;
        foreach (LevelReguards.Reguard item in lr.ArrayRg[i])
        {
            GameObject newButton = Instantiate(buttonPref, panel.transform);
            CustomButton cButton=newButton.GetComponent<CustomButton>();
            cButton.rg = item;


            if (sceneName == gm.CurrentSceneName)
            {
                cButton.isInScene = true;
                cButton.modelMat = mat;
            }

            newButton.GetComponent<Image>().sprite = lr.ArraySp[i][j];
            j++;

            if (item.lv > lm.currentLevel)
            {
                newButton.GetComponent<Button>().interactable = false;                
            }
            else
            {
                newButton.transform.Find("Lock").gameObject.SetActive(false);
            }

            //Going to make a child of the button with the look so I can activate it

        }

    }
}
