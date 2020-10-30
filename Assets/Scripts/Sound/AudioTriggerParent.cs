using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerParent : MonoBehaviour
{

    private AudioSource aS;
    private AudioCoordinator aC;
    // Start is called before the first frame update
    void Awake()
    {
        aS = GetComponentInParent<AudioSource>();
        aC = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioCoordinator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Enter");
            //aS.Play();
            aC.playOnTime(aS);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            aS.Stop();
        }
    }
}
