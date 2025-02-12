using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBeep : MonoBehaviour
{
    public bool beepOnStart = true;
    private float waitSeconds = 12;
    public AudioSource beep;

    void Start()
    {

        if (beepOnStart)
            StartCoroutine(PlayBeep());
    }

    public IEnumerator PlayBeep()
    {
        float timer = 0;

        while (timer < waitSeconds)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        beep.Play();
        Debug.Log("Beep");
    }

}
