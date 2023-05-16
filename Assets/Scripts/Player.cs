using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour {

    public CameraShake camerashake;
    public UIManager uimanager;
    public GameObject cam;
    public GameObject vectorback;
    public GameObject vectorforward;

    private Rigidbody rb;//online oyunlar i�in d��ardan eri�ilemez yapmak �nemlidir

    private Touch touch; //konumlar� kaydetmek i�in TouchPhase 
    [Range(15,30)]//sppedmodifier i�in range bar
    public int speedModifier;
    public int forwardSpeed;

    private bool speedballforward = false; //true - false kullanma sebebi firsttouch i�in 0 ve 1 kullan�lmas�
    private bool firsttouchcontrol = false;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()//s�rekli kontrol edece�im durum
    {
        //topun, ekrana dokundu�un anda hareketlenmesi
        if(Variables.firsttouch == 1 && speedballforward == false)
        {
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorback.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorforward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        //kamera hareket ederken vectorback ve vectorforward da hareket halinde olmal�
        }


        if (Input.touchCount > 0)//ekrana ka� adet parmak dokundu�u girdisi
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)//parmak dokundu�u an ilerleme ba�lamas�
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//e�er parmak gameobject �zerinde de�ilse
                {
                    if (firsttouchcontrol == false) //sadece bir defa �al��mas� i�in
                    {
                        Variables.firsttouch = 1;
                        uimanager.First();//fonksiyonu uimanager'dan player'a �a��rma
                        firsttouchcontrol = true;
                    }
                    
                }
                
            }

            else if (touch.phase == TouchPhase.Moved)// telefon ekran�ndaki x ve y noktas�n�, oyundaki x ve z noktas�na tan�mlama 
            {
                
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//e�er parmak gameobject �zerinde de�ilse
                {

                    rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                                          transform.position.y,
                                          touch.deltaPosition.y * speedModifier * Time.deltaTime);

                    if (firsttouchcontrol == false) 

                    {
                        Variables.firsttouch = 1;
                        uimanager.First();

                        firsttouchcontrol = true;
                    }

                }
            }
            else if (touch.phase == TouchPhase.Ended )
            {
                rb.velocity = new Vector3(0, 0, 0);
                //rb.velocity = Vector3.zero; kolay yaz�m
            }
           
        }
        //Transform Position kullan�ld���nda top nesnelerin i�inden ge�ebiliyor.
        //Fizi�i rigidbody �zerinden d�nd�rmek i�in Velocity.
    }

    public GameObject[] FractureItems;//patlama efekti ile sa��lacak par�alar i�in listeleme y�ntemi
    public void OnCollisionEnter(Collision hit)//topun nesneye(var olan bir �eye) �arp�nca yok olmas�
    {
        //metodu en kolay kontrol etme y�ntemi tag olu�turmak
        if (hit.gameObject.CompareTag("enemy"))
        {
            camerashake.CameraShakesCall();
            uimanager.StartCoroutine("WhiteEffect");
            gameObject.transform.GetChild(0).gameObject.SetActive(false);//topun d�� katman�n nesneye de�ince yok olmas�
            foreach (GameObject item in FractureItems)
            {
                //listedeki her item i�in, oyun ba�lar ba�lamaz patlama efekti ya�anmas�n� �nleme ve nesneye �arpt���nda ger�ekle�tirme
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
               
            }
            StartCoroutine("TimeScaleControl");
             //zaman kontrolleri i�in coroutine   
        }
    }
    //restart i�lemini top patlad���na direkt ekrana vermek yerine
    //birka� saniye zaman ge�mesini ve
    //ondan sonra ekrana vermeyi sa�lama
    public IEnumerator TimeScaleControl()
    {
        speedballforward = true;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 0.4f; //oyundaki zaman� yava�latma
        yield return new WaitForSecondsRealtime(0.6f); //butonun yava� gelmesini sa�lama
        uimanager.RestartButtonActive();
        rb.velocity = Vector3.zero; //�arpt���nda top h�z�n� s�f�rlama
    }
}
