using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS_Move : MonoBehaviour
{
    private TestLocationService tS;
    private Text oD;

    private bool engaged;

    private float gpsX;
    private float gpsZ;
    private float offsetX;
    private float offsetZ;
    private float x;
    private float z;

    // Start is called before the first frame update
    void Awake()
    {
        tS = GameObject.FindGameObjectWithTag("GameController").GetComponent<TestLocationService>();
        oD = GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>();
        engaged = false;
        StartCoroutine(setOffset());
    }

    // Update is called once per frame
    void Update()
    {
        if (engaged) 
        {
            StartCoroutine(updateGPS());
            engaged = false;
        }
    }

    private void updatePos() 
    {
        gpsX -= offsetX;
        gpsZ -= offsetZ;
        x = ConvertGPSCoords(gpsX);
        z = ConvertGPSCoords(gpsZ);
        transform.position = new Vector3(x, 0, z);
        float f = Vector2.Distance(new Vector2(x, z), new Vector2(0, 30));
        oD.text = f.ToString();
    }

    private float ConvertGPSCoords(float n) 
    {
        return n*(111190f);
    }

    private IEnumerator setOffset() 
    {
        yield return new WaitForSeconds(1);
        offsetX = tS.GPSX;
        offsetZ = tS.GPSZ;
        engaged = true;
    }

    private IEnumerator updateGPS() 
    {
        yield return new WaitForSeconds(1);
        gpsX = tS.GPSX;
        gpsZ = tS.GPSZ;
        updatePos();
        StartCoroutine(updateGPS());
    }
}
