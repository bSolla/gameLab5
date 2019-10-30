using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    private float timer;
    public float timeBetweenMovement = 1;
    private float Dir = 1f;
    public float XMin;
    public float XMax;
    
    void Start()
    {
        Dir = Random.value > 0.5f ? 1f : -1f;
        float StartX = Random.Range (XMin, XMax);
        transform.position = new Vector2 (StartX, transform.position.y);
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //movement = new Vector2 (Random.Range (-10, 10f), 6);
            //timer += timeBetweenMovement;
        }
        //transform.position = Vector2.Lerp (transform.position, movement, speed * Time.fixedDeltaTime);
        if (transform.position.x > XMax)
        {
            Dir = -1f;
        }
        else if (transform.position.x < XMin)
        {
            Dir = 1f;
        }

        Vector2 newPos = new Vector2 (transform.position.x + ((speed * Time.deltaTime) * Dir), transform.position.y);
        transform.position = newPos;
    }

    /* void Move()
    {
        Debug.Log ("moving");
        transform.position = new Vector2 (newPos.x + Mathf.Sin(Time.time * speed), transform.position.y);

        if (transform.position.x > 100f)
        {
            Debug.Log ("moving left");
            transform.position = new Vector2 (transform.position.x, transform.position.y);
        }
        else if (transform.position.x < -100f)
        {
            Debug.Log ("moving right");
            transform.position = new Vector2 (transform.position.x, transform.position.y);
        }
    } */
}
