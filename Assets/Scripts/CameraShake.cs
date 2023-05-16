using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool shakecontrol = false;
    public IEnumerator CameraShakes(float duration, float magnitude)//kamera titre�iminde ge�ecek zaman ve �iddet b�y�kl���
    {
        Vector3 originalPos = transform.localPosition;//ana kameran�n pozisyonuna e�it olmal�

        float elapsed = 0.0f;//ge�en zaman

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;// x ve y y�n�nde titre�im olu�turma
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);//x ve y pozisyonlar�nda harekete ge�ece�i i�in z ellemiyoruz

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
