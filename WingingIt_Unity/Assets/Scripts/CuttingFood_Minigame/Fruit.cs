using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] GameObject slicePref;
    [SerializeField] float points=10;
    float speedRotate;

    private void Start()
    {
        float num = Random.Range(0, 2);
        if (num == 1)
        { speedRotate = Random.Range(30, 60);}
        else { speedRotate = Random.Range(-60, -30);}
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Knife")
        {
            //Vector3 dir = (other.transform.position - this.transform.position).normalized;
            //Quaternion rot = Quaternion.LookRotation(dir);

            GameObject slices= Instantiate(slicePref,this.transform.position, this.transform.rotation);//rot
            Destroy(slices, 2f);

            FindObjectOfType<CuttingGameManager>().AddPoints(points);

            Destroy(this.gameObject);
        }
    }  
}
