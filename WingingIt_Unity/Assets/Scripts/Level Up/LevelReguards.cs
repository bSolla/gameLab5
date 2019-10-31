//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                    A U T H O R  &  N O T E S
//                                   coded by Paula, october 2019
//    Gets all the textures in the resources folder and stores them, when you level up it gives you the reguard
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReguards : MonoBehaviour
{
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                V A R I A B L E S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public enum TextureType { Coop, Door, Fences, Inside, Nest, Perch};           //Add whatever you can change the texture, im using this for testing

    /*[Header ("Foreach reguard fill the 3 arrays")]
    public Texture[] textureReguards;
    public TextureType[] textureType;
    public int[] levelNecesary;


    Texture[] coopTexture;
    Texture[] doorTexture;
    Texture[] roofTexture;
    Texture[] feederTexture;

    Texture[][] arraysTexture= new Texture[][] {coopTexture, doorTexture, roofTexture, feederTexture};*/


    

    public class Reguard
    {
        public Texture tx;
        public int lv;
        public TextureType type;
    }

    List<Reguard> coopRg = new List<Reguard>();
    List<Reguard> doorRg = new List<Reguard>();
    List<Reguard> fencesRg = new List<Reguard>();
    List<Reguard> insideRg = new List<Reguard>();
    List<Reguard> nestRg = new List<Reguard>();
    List<Reguard> perchRg = new List<Reguard>();

    List<Reguard>[] arrayRg = new List<Reguard>[6];
    Sprite[][] arraySp;

    public List<Reguard>[] ArrayRg { get => arrayRg; set => arrayRg = value; }
    public Sprite[][] ArraySp { get => arraySp; set => arraySp = value; }

    //public Sprite[][] ArraySp { get => arraySp; set => arraySp = value; }



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


    void Start()
    {
        MakeReguardsLists();
        MakeSpritesArrays();        
    }

    //This method put the lists of textures in one array and search all the textures in the resources folder, then with two foreach loops creates all the reguards based the folder and the name of the textures
    void MakeReguardsLists()
    {
        ArrayRg[0] = coopRg;
        ArrayRg[1] = doorRg;
        ArrayRg[2] = fencesRg;
        ArrayRg[3] = insideRg;
        ArrayRg[4] = nestRg;
        ArrayRg[5] = perchRg;


        Texture[] t0 = Resources.LoadAll<Texture>("Level Reguards/Textures/coopTx");
        Texture[] t1 = Resources.LoadAll<Texture>("Level Reguards/Textures/doorTx");
        Texture[] t2 = Resources.LoadAll<Texture>("Level Reguards/Textures/fencesTx");
        Texture[] t3 = Resources.LoadAll<Texture>("Level Reguards/Textures/insideTx");
        Texture[] t4 = Resources.LoadAll<Texture>("Level Reguards/Textures/nestTx");
        Texture[] t5 = Resources.LoadAll<Texture>("Level Reguards/Textures/perchTx");


        Texture[][] txArray = new Texture[][] { t0, t1, t2, t3, t4, t5 };

        //This send to the StoreColors script in the gm the first texture of each array
        GetComponent<StoreColors>().StoreOriginalTextures(t0[0], t1[0], t2[0], t3[0], t4[0], t5[0]);//t1[0],t2[0]....

        int i = 0;

        foreach (List<Reguard> item in ArrayRg)
        {
            foreach (Texture tx in txArray[i])
            {
                //Here I convert the name of the texture in a char array, then I pick the two first chars and I convert them into an int to know the level when you unlock them
                char[] ch = tx.name.ToCharArray();
                string st = ch[0].ToString() + ch[1].ToString();

                int j; int.TryParse(st, out j);


                Reguard r = new Reguard();
                r.tx = tx;
                r.lv = j;

                //This switch asign the type of texture it is depending in what list the reguard is
                switch (i)
                {
                    case 0:
                        r.type = TextureType.Coop;
                        break;

                    case 1:
                        r.type = TextureType.Door;
                        break;

                    case 2:
                        r.type = TextureType.Fences;
                        break;

                    case 3:
                        r.type = TextureType.Inside;
                        break;

                    case 4:
                        r.type = TextureType.Nest;
                        break;

                    case 5:
                        r.type = TextureType.Perch;
                        break;

                    default:
                        break;
                }


                ArrayRg[i].Add(r);
            }
            i++;
        }        
    }

    void MakeSpritesArrays()
    {
        ArraySp = new Sprite[6][];

        ArraySp[0] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/coopSp");
        ArraySp[1] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/doorSp");
        ArraySp[2] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/fencesSp");
        ArraySp[3] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/insideSp");
        ArraySp[4] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/nestSp");
        ArraySp[5] = Resources.LoadAll<Sprite>("Level Reguards/Sprites/perchSp");

        //Make the rest of the arrays
    }



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<       L E V E L   U P !        >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //This method is called from the LevelManager, it goes for all the list checking if the level to unlook the reguard is the same as the current level and send the reguards to the lm
    public List<Reguard> GetReguards(int level) //Maybe return the reguards(?)
    {
        List<Reguard> returnList= new List<Reguard>();
        foreach (List<Reguard> item in ArrayRg)
        {
            foreach (Reguard rg in item)
            {
                if (rg.lv>level)
                {
                    break;
                }

                if (rg.lv == level)
                {
                    returnList.Add(rg);
                }
            }
        }
        return returnList;
    }
}
