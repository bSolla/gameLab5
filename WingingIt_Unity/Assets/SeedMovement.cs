using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    private float timer;
    public float timeBetweenMovement = 1;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            movement = new Vector2 (Random.Range (-10, 10f), 6);
            timer += timeBetweenMovement;
        }

        transform.position = Vector2.Lerp (transform.position, movement, speed * Time.fixedDeltaTime);
    }
}
