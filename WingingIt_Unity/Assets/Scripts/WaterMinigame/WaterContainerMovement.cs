using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterContainerMovement : MonoBehaviour
{
    public float speed, xBound;
    public float offset = 0.05f;
    public bool following = false;
    public WaterMiniGameManager manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
          /* if (!manager.gameOver)
          {
            
          } */
          following = true;
          
        }

      if (following)
      {
          float dist = transform.position.z - Camera.main.transform.position.z;
          Vector3 pos = Input.mousePosition;
          pos.z = dist;
          pos = Camera.main.ScreenToWorldPoint(pos);
          pos.y = transform.position.y;
          transform.position = pos;
      }
    }

    void OnParticleCollision (GameObject other)
    {
        Debug.Log ("touched by water");
        manager.score += manager.scoreGained;
    }
}
