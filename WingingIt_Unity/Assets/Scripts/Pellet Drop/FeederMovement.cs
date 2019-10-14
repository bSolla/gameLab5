using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederMovement : MonoBehaviour
{
    //private Rigidbody2D myRigidbody;
    public float speed, xBound;
    public float offset = 0.05f;
    public bool following = false;
    public PelletDropManager manager;
    public ParticleSystem seeds;
    public List <ParticleCollisionEvent> collisionEvents;
    
    // Start is called before the first frame update
    void Start()
    {
        //collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
    
      if (Input.GetMouseButtonDown(0))
      {
          if (!manager.gameOver)
          {
              following = true;
          }
          
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

    public void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Seeds")
        {
            
        }
    }

    void OnParticleCollision (GameObject other)
    {
        Debug.Log ("omg a seed touched me");
        manager.score += manager.scoreGained;
        //Destroy(other);
       /*  int numCollisionEvents = seeds.GetCollisionEvents (other, collisionEvents);
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce (force);
            }
            i++;
        } */
    }
}
