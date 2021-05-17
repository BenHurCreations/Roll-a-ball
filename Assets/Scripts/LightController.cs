using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class LightController : MonoBehaviour
{
    // Lightning variables
    public GameObject lightVolume;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Capsule"))
        {
            if (lightVolume.activeSelf == true)
            {
                lightVolume.SetActive(false);
            }
            else
            {
                lightVolume.SetActive(true);
            }
        }
    }
}