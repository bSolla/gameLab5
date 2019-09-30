using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] float points = -20;
    float speedRotate;

    Animator lightAnim;

    private void Start()
    {
        float num = Random.Range(0, 2);
        if (num == 1)
        { speedRotate = Random.Range(30, 60); }
        else { speedRotate = Random.Range(-60, -30); }

        lightAnim = FindObjectOfType<Animator>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Knife")
        {

            GameObject slices = Instantiate(particles, this.transform.position, this.transform.rotation);
            Destroy(slices, 2f);

            FindObjectOfType<CuttingGameManager>().AddPoints(points);

            Destroy(this.gameObject);
            lightAnim.SetTrigger("Play");
        }
    }
}
