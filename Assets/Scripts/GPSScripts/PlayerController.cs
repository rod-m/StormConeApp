using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using MapTools;

public class PlayerController : MonoBehaviour
{
    private Vector3 posStart = Vector3.zero;
    private Vector3 posMove = Vector3.zero;
    private Vector3 posChange = Vector3.zero;

    //private Transform miniCam;

    public Canvasmanager canvasmanager;
    private bool canvasReady = false;

    private Transform soundSource1;
    private Camera miniCam;
    public float orthoSize = 30f;
    private bool GPSReady = false;
    private bool soundSource1Found = false;
    private bool miniCamFound = false;
    string log = "";
    private int countFrame = 0;
    int delayAcccuracy = 10;
    private float deg = 0f;
    
    private void Awake()
    {
        GetCanvas();
    }

    private void OnEnable()
    {
        GetCanvas();
    }

    void MoveGPS()
    {
        posChange.x = (float) MercatorProjection.lonToX(Input.location.lastData.longitude);
        posChange.z = (float) MercatorProjection.latToY(Input.location.lastData.latitude);
        posMove = posStart - posChange;
        transform.position = posMove;
        if (soundSource1Found)
        {
            UpdateDistances($"Distance to Source: {Vector3.Distance(posMove, soundSource1.position)}m");
        }
        else
        {
            UpdateDistances($"Sound Source Missing!");
            return;
        }

        
        if (Input.touchCount >= 2)
        {
            Vector2 touch0, touch1;
            float distance;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            distance = Vector2.Distance(touch0, touch1);
            if (miniCamFound)
            {
                miniCam.orthographicSize = orthoSize + distance;
            }
        }
 
    }
    IEnumerator DoCompassEn()
    {
        while (true)
        {
            DoCompass();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void DoCompass()
    {
        log = $"POS: {posMove.ToString()} \n";
        deg = -Input.compass.trueHeading;
        deg = Mathf.Round(deg);
        log += $"deg: {(360 - deg)}";
        if (miniCamFound)
        {
            miniCam.transform.rotation = Quaternion.Euler(90, deg, 0);
        }
        else
        {
            log = "Missing Mini Camera!";
        }

        UpdateLog(log);
    }

    void GetCanvas()
    {
        if (canvasmanager == null)
        {
            posStart.x = (float) MercatorProjection.lonToX(Input.location.lastData.longitude);
            posStart.z = (float) MercatorProjection.latToY(Input.location.lastData.latitude);

            var gm = GameObject.Find("LocationSeviceTest");
            if (gm != null)
            {
                canvasmanager = gm.GetComponent<Canvasmanager>();
                canvasmanager.m_LocationText.text = "Player Marker Added!";
                canvasReady = true;
            }
            else
            {
                Debug.Log("Error: No Canvas Manager!");
            }

            var mn = GameObject.Find("MiniCamera");
            if (mn != null)
            {
                miniCam = mn.GetComponent<Camera>();
                miniCamFound = true;
                StartCoroutine(DoCompassEn());
            }
            else
            {
                Debug.Log("Error: No MiniCamera!");
            }

            var ss = GameObject.Find("sound source");
            if (ss != null)
            {
                soundSource1 = ss.transform;
                soundSource1Found = true;
            }
            else
            {
                Debug.Log("Error: No sound source!");
            }
        }
    }

    void UpdateLog(string txt)
    {
        if (canvasReady)
        {
            canvasmanager.m_LogText.text = txt;
        }
    }

    void UpdateDistances(string txt)
    {
        if (canvasReady)
        {
            canvasmanager.m_distanceText.text = txt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveGPS();
    }
}