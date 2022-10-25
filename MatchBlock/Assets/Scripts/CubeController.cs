using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public List<CubeManager> cubeList = new List<CubeManager>();

    //SpawnPointte oluþturduðumuz kübü seçili küp olarak elimizde tutuyoruz.
    public CubeManager currentCube;
    
    //Map objesine eklediðimiz spawnPoint adlý boþ objenin Transform bilgisi
    public Transform spawnPoint;
    
    //Dokunma eylemleri için eklediðimiz Touch nesnesi
    private Touch touch;
    private Vector3 downPosition, upPosition;
    
    //Parmaðýmýzla basýp basmadýðýmýzý kontrol eden bool deðiþkenimiz
    private bool dragStarted;
    void Start()
    {
        currentCube = SelectRandomCube();
    }

    void Update()
    {
        //Dokunma iþlemi baþladýmý
        if (Input.touchCount>0)
        {
            //ilk dokunma eylemini oluþturduðumuz touch nesnemize atadýk
            touch = Input.GetTouch(0);
            
            //Eðer kullanýcý ilk defa dokunmuþssa o dragStarted deðiþkenimiz yani harekete baþlama deðiþkenimizi true yapmamýz gerekir.
            if (touch.phase==TouchPhase.Began)
            {
                dragStarted = true;

                //Ýlk dokunmada iki vectorümüzde touch pozisyonumuza atanýr. Sonuçta sadece dokunuyor sürükleme daha baþlamadý
                downPosition = touch.position;
                upPosition = touch.position;
            }

            //Bool deðiþkenimiz true olduysa iki ihtimal vardýr. Ya parmaðý hareket ettiririz ya da parmaðýmýzý çekeriz.
            if (dragStarted==true)
            {
                //Parmaðý Hareket Durumu
                if (touch.phase==TouchPhase.Moved)
                {
                    downPosition = touch.position;
                }

                //Parmaðý kaldýrma durumu
                if (touch.phase == TouchPhase.Ended)
                {
                    //FIRLATMA ÝÞLEMÝ
                    downPosition = touch.position;
                    dragStarted = false;

                    //Eðer seçili küp yoksa herhangi bir iþlem yapmasýn
                    if (!currentCube) return;

                    //Ýlk baþta kübümüzün hýzýný sýfýrlýyoruz çünkü parmaðýmýzla hareket ettirdiðimizdeki hýzýda alýrsak istediðimiz þekilde hareket etmez
                    currentCube.rb.velocity = Vector3.zero;
                    currentCube.GoCube();

                    currentCube = null;
                    StartCoroutine(WaitCube());

                   
                }

                //Parmaðýmýzý hareket ettirdiðimizde neyin hareket edeceðini daha belirmediðimizden seçili kübümüzü kontrol ediyoruz.
                if (currentCube)
                {
                    currentCube.rb.velocity = CalculateDirection() * 5; //5 Hýzýmýz oluyor
                }
            }
        }
    }

    //Random bir küp seçmek için fonksiyonumuz
    private CubeManager SelectRandomCube()
    {
        GameObject temp = Instantiate(cubeList[Random.Range(0, cubeList.Count)].gameObject,spawnPoint.position, Quaternion.identity);
        return temp.GetComponent<CubeManager>();
    }

    //Elimizde ki kübü yolladýktan sonra belli bir süre yeni küp oluþmamasý için IEnumetator kullanýyoruz
    private IEnumerator WaitCube()
    {
        yield return new WaitForSeconds(1);
        currentCube = SelectRandomCube();

    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp = (downPosition - upPosition).normalized;
        temp.y = 0;
        temp.z = 0;
        return temp;
    }
}
