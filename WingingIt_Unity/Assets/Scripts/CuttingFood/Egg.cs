using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] float points = -20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Knife")
        {

            GameObject part = Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(part, 0.5f);

            FindObjectOfType<CuttingGameManager>().AddPoints(points);

            Destroy(this.gameObject);
        }
    }
}
