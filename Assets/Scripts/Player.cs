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

    private Rigidbody rb;//online oyunlar için dýþardan eriþilemez yapmak önemlidir

    private Touch touch; //konumlarý kaydetmek için TouchPhase 
    [Range(15,30)]//sppedmodifier için range bar
    public int speedModifier;
    public int forwardSpeed;

    private bool speedballforward = false; //true - false kullanma sebebi firsttouch için 0 ve 1 kullanýlmasý
    private bool firsttouchcontrol = false;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()//sürekli kontrol edeceðim durum
    {
        //topun, ekrana dokunduðun anda hareketlenmesi
        if(Variables.firsttouch == 1 && speedballforward == false)
        {
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorback.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorforward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        //kamera hareket ederken vectorback ve vectorforward da hareket halinde olmalý
        }


        if (Input.touchCount > 0)//ekrana kaç adet parmak dokunduðu girdisi
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)//parmak dokunduðu an ilerleme baþlamasý
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//eðer parmak gameobject üzerinde deðilse
                {
                    if (firsttouchcontrol == false) //sadece bir defa çalýþmasý için
                    {
                        Variables.firsttouch = 1;
                        uimanager.First();//fonksiyonu uimanager'dan player'a çaðýrma
                        firsttouchcontrol = true;
                    }
                    
                }
                
            }

            else if (touch.phase == TouchPhase.Moved)// telefon ekranýndaki x ve y noktasýný, oyundaki x ve z noktasýna tanýmlama 
            {
                
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//eðer parmak gameobject üzerinde deðilse
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
                //rb.velocity = Vector3.zero; kolay yazým
            }
           
        }
        //Transform Position kullanýldýðýnda top nesnelerin içinden geçebiliyor.
        //Fiziði rigidbody üzerinden döndürmek için Velocity.
    }

    public GameObject[] FractureItems;//patlama efekti ile saçýlacak parçalar için listeleme yöntemi
    public void OnCollisionEnter(Collision hit)//topun nesneye(var olan bir þeye) çarpýnca yok olmasý
    {
        //metodu en kolay kontrol etme yöntemi tag oluþturmak
        if (hit.gameObject.CompareTag("enemy"))
        {
            camerashake.CameraShakesCall();
            uimanager.StartCoroutine("WhiteEffect");
            gameObject.transform.GetChild(0).gameObject.SetActive(false);//topun dýþ katmanýn nesneye deðince yok olmasý
            foreach (GameObject item in FractureItems)
            {
                //listedeki her item için, oyun baþlar baþlamaz patlama efekti yaþanmasýný önleme ve nesneye çarptýðýnda gerçekleþtirme
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
               
            }
            StartCoroutine("TimeScaleControl");
             //zaman kontrolleri için coroutine   
        }
    }
    //restart iþlemini top patladýðýna direkt ekrana vermek yerine
    //birkaç saniye zaman geçmesini ve
    //ondan sonra ekrana vermeyi saðlama
    public IEnumerator TimeScaleControl()
    {
        speedballforward = true;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 0.4f; //oyundaki zamaný yavaþlatma
        yield return new WaitForSecondsRealtime(0.6f); //butonun yavaþ gelmesini saðlama
        uimanager.RestartButtonActive();
        rb.velocity = Vector3.zero; //çarptýðýnda top hýzýný sýfýrlama
    }
}
