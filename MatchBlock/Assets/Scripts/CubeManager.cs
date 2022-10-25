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
        //CompareTag �arpt���m�z nesnenin ad�n� de�il ne oldu�unu ��reniyoruz
        if (collision.gameObject.tag=="Cube")
        {
            //trygetcomponent kullanmam�z�n sebebi e�er yoksa cubemanager scripti �arpt���m�z nesnede kodumuzun patlamamas�
            if (collision.gameObject.TryGetComponent(out CubeManager cube))
            {
                if (cube.value==value)
                {
                    if (ID < cube.ID) return;                  
                    Destroy(this.gameObject);
                    Destroy(collision.gameObject);

                    if (nextCube!=null)
                    {
                        //Bo� bir nesneye atad�k sonras�nda ba�ka i�lemler yapaca��m�z i�in
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

    //F�rlatma Fonksiyonu
    public void GoCube()
    {
        rb.AddForce(transform.forward * 1000);
    }


   
}
