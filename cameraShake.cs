using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    // gem original position
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        // tilføj tilfædig position inde for enhedscirklen til kameraets position
        // så længe shake duration er over 0
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    // metode til at starte cam shake udefra
    public void ShakeCamera(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }
}
