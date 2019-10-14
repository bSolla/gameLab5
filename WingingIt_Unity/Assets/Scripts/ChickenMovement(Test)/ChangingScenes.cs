//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                           A U T H O R  &  N O T E S
//                          coded by Paula, september 2019
//    simple script to load a diferent scene, canbe used for different objects and levels
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] bool clickable = true;
    public interactionConfirmation intCon;
    public GameManager gm;


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

   public void Start ()
   {
       intCon = GetComponent<interactionConfirmation>();
   }
    private void Update()
    {
        /* if(clickable)
        {
            CheckInput();
        } */
        if (gm)
        {
            if (gm.CurrentSceneName == "Inside" || gm.CurrentSceneName == "Outside")
            {
                if (intCon.confirmed)
                {
                    Debug.Log ("eyy the bool is confirmed boyyyee");
                    FindObjectOfType<GameManager>().SaveStatsBetweenScenes();

                    SceneManager.LoadScene(sceneName);
                    this.GetComponent<interactionConfirmation>().confirmed = false;
                }
                else
                {
                    Debug.Log ("you can't interact yet my dude");
                }
            }
        }
        else if (gm == null)
        {
            Debug.Log ("no game manager in this scene, doesn't need one either aww yeah");
        }
    }
        

    //This is for putting it in an object with collider
    void CheckInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //CHANGE THIS TO A BUTTON(?)
            RaycastHit hit;
            Collider col = this.gameObject.GetComponent<Collider>();
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if (hit.collider == col && this.GetComponent<interactionConfirmation>().confirmed)
                {
                    FindObjectOfType<GameManager>().SaveStatsBetweenScenes();

                    SceneManager.LoadScene(sceneName);
                    this.GetComponent<interactionConfirmation>().confirmed = false;
                }
            }
        }        
    }


    //This is for using it in a button
    public void GoToScene(string sceneName)
    {
        FindObjectOfType<GameManager>().SaveStatsBetweenScenes();

        SceneManager.LoadScene(sceneName);
    }
}
