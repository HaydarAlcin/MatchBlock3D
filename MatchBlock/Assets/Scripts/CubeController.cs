using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public List<CubeManager> cubeList = new List<CubeManager>();

    //SpawnPointte olu�turdu�umuz k�b� se�ili k�p olarak elimizde tutuyoruz.
    public CubeManager currentCube;
    
    //Map objesine ekledi�imiz spawnPoint adl� bo� objenin Transform bilgisi
    public Transform spawnPoint;
    
    //Dokunma eylemleri i�in ekledi�imiz Touch nesnesi
    private Touch touch;
    private Vector3 downPosition, upPosition;
    
    //Parma��m�zla bas�p basmad���m�z� kontrol eden bool de�i�kenimiz
    private bool dragStarted;
    void Start()
    {
        currentCube = SelectRandomCube();
    }

    void Update()
    {
        //Dokunma i�lemi ba�lad�m�
        if (Input.touchCount>0)
        {
            //ilk dokunma eylemini olu�turdu�umuz touch nesnemize atad�k
            touch = Input.GetTouch(0);
            
            //E�er kullan�c� ilk defa dokunmu�ssa o dragStarted de�i�kenimiz yani harekete ba�lama de�i�kenimizi true yapmam�z gerekir.
            if (touch.phase==TouchPhase.Began)
            {
                dragStarted = true;

                //�lk dokunmada iki vector�m�zde touch pozisyonumuza atan�r. Sonu�ta sadece dokunuyor s�r�kleme daha ba�lamad�
                downPosition = touch.position;
                upPosition = touch.position;
            }

            //Bool de�i�kenimiz true olduysa iki ihtimal vard�r. Ya parma�� hareket ettiririz ya da parma��m�z� �ekeriz.
            if (dragStarted==true)
            {
                //Parma�� Hareket Durumu
                if (touch.phase==TouchPhase.Moved)
                {
                    downPosition = touch.position;
                }

                //Parma�� kald�rma durumu
                if (touch.phase == TouchPhase.Ended)
                {
                    //FIRLATMA ��LEM�
                    downPosition = touch.position;
                    dragStarted = false;

                    //E�er se�ili k�p yoksa herhangi bir i�lem yapmas�n
                    if (!currentCube) return;

                    //�lk ba�ta k�b�m�z�n h�z�n� s�f�rl�yoruz ��nk� parma��m�zla hareket ettirdi�imizdeki h�z�da al�rsak istedi�imiz �ekilde hareket etmez
                    currentCube.rb.velocity = Vector3.zero;
                    currentCube.GoCube();

                    currentCube = null;
                    StartCoroutine(WaitCube());

                   
                }

                //Parma��m�z� hareket ettirdi�imizde neyin hareket edece�ini daha belirmedi�imizden se�ili k�b�m�z� kontrol ediyoruz.
                if (currentCube)
                {
                    currentCube.rb.velocity = CalculateDirection() * 5; //5 H�z�m�z oluyor
                }
            }
        }
    }

    //Random bir k�p se�mek i�in fonksiyonumuz
    private CubeManager SelectRandomCube()
    {
        GameObject temp = Instantiate(cubeList[Random.Range(0, cubeList.Count)].gameObject,spawnPoint.position, Quaternion.identity);
        return temp.GetComponent<CubeManager>();
    }

    //Elimizde ki k�b� yollad�ktan sonra belli bir s�re yeni k�p olu�mamas� i�in IEnumetator kullan�yoruz
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
