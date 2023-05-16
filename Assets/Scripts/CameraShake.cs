using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool shakecontrol = false;
    public IEnumerator CameraShakes(float duration, float magnitude)//kamera titreþiminde geçecek zaman ve þiddet büyüklüðü
    {
        Vector3 originalPos = transform.localPosition;//ana kameranýn pozisyonuna eþit olmalý

        float elapsed = 0.0f;//geçen zaman

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;// x ve y yönünde titreþim oluþturma
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);//x ve y pozisyonlarýnda harekete geçeceði için z ellemiyoruz

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void CameraShakesCall()
    {
        if (shakecontrol == false)
        {
            StartCoroutine(CameraShakes(0.22f, 0.4f));
            shakecontrol = true;
        }

    }
}
