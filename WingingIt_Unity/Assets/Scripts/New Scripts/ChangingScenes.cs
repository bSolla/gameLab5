using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
    [SerializeField] string sceneName;

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //CHANGE THIS TO A BUTTON
        RaycastHit hit;
        Collider col = this.gameObject.GetComponent<Collider>();
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider == col && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(sceneName);

            }
        }
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
