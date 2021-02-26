using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingFlickering : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.2f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(1f, 2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
