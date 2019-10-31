//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                    A U T H O R  &  N O T E S
//                                   coded by Paula, october 2019
//    This script MUST BE in the gm, it stores the changes the player has made in the textures
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreColors : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Put all the textures that have to be stored
    Texture coopTexture;
    Texture doorTexture;
    Texture fencesTexture;
    Texture insideTexture;
    Texture nestTexture;
    Texture perchTexture;

    public Texture CoopTexture { get => coopTexture; }
    public Texture DoorTexture { get => doorTexture; }
    public Texture FencesTexture { get => fencesTexture; }
    public Texture InsideTexture { get => insideTexture; }
    public Texture NestTexture { get => nestTexture; }
    public Texture PerchTexture { get => perchTexture; }


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Put all the customizable items 
    public void StoreOriginalTextures(Texture coopTx, Texture doorTx, Texture fencesTx, Texture insideTx, Texture nestTx, Texture perchTx)
    {
        coopTexture = coopTx;
        doorTexture = doorTx;
        fencesTexture = fencesTx;
        insideTexture = insideTx;
        nestTexture = nestTx;
        perchTexture = perchTx;
    }


    public void StoreNewTexture(LevelReguards.Reguard rg) //Make sure there is a case for each type
    {
        switch (rg.type)
        {
            case LevelReguards.TextureType.Coop:
                coopTexture = rg.tx;
                break;

            case LevelReguards.TextureType.Door:
                doorTexture = rg.tx;
                break;

            case LevelReguards.TextureType.Fences:
                fencesTexture = rg.tx;
                break;

            case LevelReguards.TextureType.Inside:
                insideTexture = rg.tx;
                break;


            case LevelReguards.TextureType.Nest:
                nestTexture = rg.tx;
                break;

            case LevelReguards.TextureType.Perch:
                perchTexture = rg.tx;
                break;

            default:
                break;
        }
    }
}
  //Think about there are two diferent textures for the fences wich would be a problem