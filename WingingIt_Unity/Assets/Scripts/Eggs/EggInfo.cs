using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggInfo : MonoBehaviour
{
    public Material eggMaterial;
    EggManager eggManager;


    public enum EggType { Common, Rare, Legendary};
    public EggType type;
    public int eggNum;

    public bool owned;

    private void Start()
    {
        GetComponentInChildren<Renderer>().material=eggMaterial;
        eggManager = FindObjectOfType<EggManager>();
    }
    private void Update()
    {
        if(eggManager.currentEgg == null)
        {
            eggManager.currentEgg = this.gameObject;
        }
    }

}
