using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseUI : MonoBehaviour
{
    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }
}

//This is not worth to comment
