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


    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //                                  M E T H O D S 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

   
    private void Update()
    {
        if(clickable)
        {
            CheckInput();
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
                if (hit.collider == col)
                {
                    FindObjectOfType<GameManager>().SaveStatsBetweenScenes();

                    SceneManager.LoadScene(sceneName);
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
