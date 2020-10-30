using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCoordinator : MonoBehaviour
{

    private bool isStarted;
    private float timePassed;
    private float endRef;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        endRef = 0;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        Debug.Log(timePassed);
        if (endRef != 0 && timePassed > endRef) 
        {
            isStarted = false;
            timePassed = 0;
            //transform.position = startPos;
        }
    }

    public void playOnTime(AudioSource aS) 
    {
        if (isStarted)
        {
            aS.time = timePassed;
            aS.Play();
        }
        else 
        {
            endRef = aS.clip.length;
            isStarted = true;
        }
    }
}
