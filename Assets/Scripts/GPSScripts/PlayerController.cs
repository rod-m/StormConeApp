using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using MapTools;
public class PlayerController : MonoBehaviour
{
    private Vector3 posStart;
    private Vector3 posMove = Vector3.zero;
    private Vector3 posChange = Vector3.zero;
    public float GPSX;
    public float GPSZ;
    private Transform miniCam;

    private Text m_LogText;
//
    private Text m_LocText;
    private Transform soundSource1;
 
    private bool GPSReady = false;
    // Start is called before the first frame update
    private void Awake()
    {
        miniCam = GameObject.Find("MiniCamera").transform;
        soundSource1 = GameObject.Find("sound source").transform;
        m_LogText = GameObject.Find("Log text").GetComponent<Text>();
        m_LocText = GameObject.Find("Object Distance").GetComponent<Text>();
        m_LocText.text = "Marker Added";
        posStart = Vector3.zero;
        GPSX = Input.location.lastData.latitude;
        GPSZ = Input.location.lastData.longitude;
        posStart.x = (float) MercatorProjection.lonToX(Input.location.lastData.longitude);
        posStart.z = (float) MercatorProjection.latToY(Input.location.lastData.latitude);

    }
    void MoveGPS()
    {
        posChange.x = (float) MercatorProjection.lonToX(Input.location.lastData.longitude);
        posChange.z = (float) MercatorProjection.latToY(Input.location.lastData.latitude);
        posMove =  posStart - posChange;
        transform.position = posMove;
        m_LocText.text = $"Dist: {Vector3.Distance(posMove, soundSource1.position)}";
        m_LogText.text = $"POS: {posMove.ToString()}";
        miniCam.rotation = Quaternion.Euler(90, -Input.compass.trueHeading, 0);
    }
    // Update is called once per frame
    void Update()
    {
        MoveGPS();
        
    }

   
}
