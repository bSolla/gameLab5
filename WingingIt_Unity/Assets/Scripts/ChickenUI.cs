using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenUI : MonoBehaviour
{
    public GameObject ThinkBubble;
    Image moodImg;
    public Sprite[] moodSprites;    //Hungry=0 - Thirsty=1  -  Sad=2
    void Start()
    {
        StopAskForHelpUI();

        ThinkBubble = this.gameObject.transform.GetChild(0).gameObject;
        moodImg = ThinkBubble.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public void AskForHelpUI(int whatMood)
    {
        ThinkBubble.SetActive(true);
        moodImg.sprite = moodSprites[whatMood];
    }
    public void StopAskForHelpUI()
    {
        ThinkBubble.SetActive(false);
    }
}
