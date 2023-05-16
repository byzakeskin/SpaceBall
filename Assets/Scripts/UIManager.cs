using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public Animator LayoutAnimator;

    public void LayoutSettingsOpen()
    {
        LayoutAnimator.SetTrigger("slide_in");//triggerlama
    }

    public void LayoutSettingsClose()
    {
        LayoutAnimator.SetTrigger("slide_out");//triggerlama
    }

    public Text point;

    //ayarlar k�sm�n� a��p kapama
    public GameObject settings_open;
    public GameObject settings_close;
    public GameObject layout_Background;
    //sesi kapat�p a�ma
    public GameObject sound_on;
    public GameObject sound_off;
    //titre�imi kapat�p a�ma
    public GameObject vibration_on;
    public GameObject vibration_off;
    public GameObject iap;
    public GameObject info;
    public GameObject hand;
    
    //restart 
    public GameObject restart;

   

    public void Start()
    {
        if(PlayerPrefs.HasKey("Sound") == false)//de�i�kenin var olup olmad���n� kontrol etme
        {
            PlayerPrefs.SetInt("Sound", 1);//oyun ilk defa a��ld� sound verisi yoksa (ba�lang��ta yok) int 1 ata
        }

        pointupdate();
    }

    public void First()
    {
        hand.SetActive(false);
        settings_open.SetActive(false);
        settings_close.SetActive(false);
        layout_Background.SetActive(false);
        sound_on.SetActive(false);
        sound_off.SetActive(false);
        vibration_on.SetActive(false);
        vibration_off.SetActive(false);
        iap.SetActive(false);
        info.SetActive(false);
    }

    public void pointupdate()
    {
        point.text = PlayerPrefs.GetInt("starpoint").ToString();
    }

    public void RestartButtonActive()
    {
        restart.SetActive(true);
    }
    
    public void RestartScene()
    {
        Variables.firsttouch = 0;
        Time.timeScale = 1f;//oyunun yeniden ba�lad���nda ayn� h�zda �al��maya devam etmesi i�in
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//aktif sahneyi resetleme
    }

    

    

    

    //public void Privacy_Policy()
    //{
    //    Application.OpenURL("linlinklink");  link a�mak i�in i�e yarayan kodumuz
    //}

    //Buton fonksiyonlar�
    public void Settings_open()
    {
        settings_open.SetActive(false);
        settings_close.SetActive(true);
        LayoutAnimator.SetTrigger("slide_in");//animasyonun �al��mas� i�in trigger kullan�yoruz (butona bas�ld���nda tetiklenir)
    //butonlar i�in kay�t sistemi
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            sound_on.SetActive(true);
            sound_off.SetActive(false);
            AudioListener.volume = 1;
        }
        
        else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            sound_on.SetActive(false);
            sound_off.SetActive(true);
            AudioListener.volume = 0;
        }
    }

    public void Settings_close()
    {
        settings_open.SetActive(true);
        settings_close.SetActive(false);
        LayoutAnimator.SetTrigger("slide_out");
    }

    //ses a��p kapama
    public void Sound_on()
    {
        sound_on.SetActive(false);
        sound_off.SetActive(true);
        AudioListener.volume = 0; // oyun i�i sesleri kapatma komutu
        PlayerPrefs.SetInt("Sound", 2); 
    }

    public void Sound_off()
    {
        sound_on.SetActive(true);
        sound_off.SetActive(false);
        AudioListener.volume = 1; // oyun i�i sesleri a�ma komutu
        PlayerPrefs.SetInt("Sound", 1);
    }

    public void Vibration_on()
    {
        vibration_on.SetActive(false);
        vibration_off.SetActive(true);
    }

    public void Vibration_off()
    {
        vibration_on.SetActive(true);
        vibration_off.SetActive(false);
    }
    //haskey
    //getkey > veriyi getirir
    //setkey > veriyi yerle�tirir

}
