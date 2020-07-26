using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GPSLocation
{
    public class TestLocationService : MonoBehaviour
    {
        [SerializeField]
        Text m_LocationText;

        public Text locationText
        {
            get { return m_LocationText; }
            set { m_LocationText = value; }
        }

        [SerializeField]
        Text m_LogText;

        public Text logText
        {
            get { return m_LogText; }
            set { m_LogText = value; }
        }

        void Log(string message)
        {
            m_LogText.text += $"{message}\n";
        }

        IEnumerator CheckGPSSupport()
        {
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                Log("\nGPS Not Enabled!");
                yield break;
            }
              

            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Log("\nTimed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Log("\nUnable to determine device location");
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                Log("\nLocation: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }

            // Stop service if there is no need to query location updates continuously
        
           // Input.location.Stop();
           StartCoroutine(CheckGPS());
        }

        IEnumerator CheckGPS()
        {
            while (true)
            {
                m_LocationText.text = "Location: " + Input.location.lastData.latitude + " " +
                                      Input.location.lastData.longitude;
                //              " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy;
                yield return new WaitForSeconds(1);
            }
        }

        void OnEnable()
        {
           
                //Log("\nGPS Available?");
                StartCoroutine(CheckGPSSupport());
            
        }
    }
}