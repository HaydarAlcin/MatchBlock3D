using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public int value;
    public GameObject nextCube;
    public Rigidbody rb;
    public int ID;
    void Awake()
    {
        ID = GetInstanceID();
        rb = GetComponent<Rigidbody>();
    }

    public void CreatingForce()
    {
        rb.AddForce((Vector3.up + Vector3.forward) * 150);
    }



    private void OnCollisionEnter(Collision collision)
    {
        //CompareTag çarptýðýmýz nesnenin adýný deðil ne olduðunu öðreniyoruz
        if (collision.gameObject.tag=="Cube")
        {
            //trygetcomponent kullanmamýzýn sebebi eðer yoksa cubemanager scripti çarptýðýmýz nesnede kodumuzun patlamamasý
            if (collision.gameObject.TryGetComponent(out CubeManager cube))
            {
                if (cube.value==value)
                {
                    if (ID < cube.ID) return;                  
                    Destroy(this.gameObject);
                    Destroy(collision.gameObject);

                    if (nextCube!=null)
                    {
                        //Boþ bir nesneye atadýk sonrasýnda baþka iþlemler yapacaðýmýz için
                        GameObject temp = Instantiate(nextCube, transform.position, Quaternion.identity);
                        if (temp.TryGetComponent(out CubeManager newCube))
                        {
                            newCube.CreatingForce();
                        }
                    }
                }
            }
        }
    }

    //Fýrlatma Fonksiyonu
    public void GoCube()
    {
        rb.AddForce(transform.forward * 1000);
    }


   
}
