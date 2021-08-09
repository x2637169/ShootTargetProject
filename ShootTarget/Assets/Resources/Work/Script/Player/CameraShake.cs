using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [HideInInspector] public Vector3 orignalPos;
    [HideInInspector] public Quaternion orignalRotaiton;

    Menu menu;

    void Awake()
    {
        menu = GameObject.FindWithTag("Manager").GetComponent<Menu>();
        orignalPos = transform.localPosition;
        orignalRotaiton = transform.localRotation;
    }

    public IEnumerator CamShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0 || menu.Win || menu.Dead) yield break;
            if (menu.Stop) yield return null;

            float posX = Random.Range(-1f, 1f) * magnitude * Time.unscaledDeltaTime;
            float posY = Random.Range(-1f, 1f) * magnitude * Time.unscaledDeltaTime;
            transform.localPosition = new Vector3(posX, posY, orignalPos.z);

            float rotX = Random.Range(-5f, 5f) * magnitude * Time.unscaledDeltaTime;
            float rotY = Random.Range(-5f, 5f) * magnitude * Time.unscaledDeltaTime;
            float rotZ = Random.Range(-5f, 5f) * magnitude * Time.unscaledDeltaTime;
            transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        transform.localPosition = orignalPos;
        transform.localRotation = orignalRotaiton;
    }
}
