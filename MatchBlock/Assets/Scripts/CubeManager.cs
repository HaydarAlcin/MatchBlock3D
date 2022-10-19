using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public int value;
    public GameObject nextCube;
    public Rigidbody rb;
    public int ID;
    void Start()
    {
        ID = GetInstanceID();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //CompareTag çarptýðýmýz nesnenin adýný deðil ne olduðunu 
        if (collision.gameObject.CompareTag("Cube"))
        {
            if (collision.gameObject.TryGetComponent(out CubeManager cube))
            {
                if (cube.value==value)
                {
                    if (ID<cube.ID)
                    {

                    }
                }
            }
        }
    }
}
