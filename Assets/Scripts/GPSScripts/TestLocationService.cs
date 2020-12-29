using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace GPSScripts
{
    public class TestLocationService : MonoBehaviour
    {
        [SerializeField]
        Text m_LocationText;
        [SerializeField]
        Button m_StartGPSButton;


        public float GPSX;
        public float GPSZ;

        public bool test;
        public GameObject player;

        private void Start()
        {
            GPSX = 0;
            GPSZ = 0;
        }


        public Button startGPSButton
        {
            get { return m_StartGPSButton; }
            set { m_StartGPSButton = value; }
        }
        void SetStartGPSButtonActive(bool active)
        {
            if (m_StartGPSButton != null)
                m_StartGPSButton.gameObject.SetActive(active);
        }
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
            m_LogText.text = "Starting GPS";
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                m_LogText.text = "*** GPS Not Enabled By USER! ***";

                yield return new WaitForSeconds(3);
                m_LocationText.text = "NO GPS";
                m_LogText.text = "";

                yield break;
            }


            // Start service before querying location
            Input.location.Start(1, 1);

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                Debug.Log("Waiting " + maxWait);
                m_LocationText.text = "Waiting... " + maxWait;
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Log("\nTimed out");
                m_LocationText.text = "GSP Timeout 20s!";
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Log("\nUnable to determine device location");
                m_LocationText.text = "Unable to determine device location";
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
                if (test)
                {
                    //Instantiate(player);
                    player.GetComponent<GPS_Move>().Activate();
                    test = false;
                }
                GPSX = Input.location.lastData.latitude;
                GPSZ = Input.location.lastData.longitude;
                m_LocationText.text = "Location: " + Input.location.lastData.latitude + " " +
                                      Input.location.lastData.longitude;
                //              " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy;
                yield return new WaitForSeconds(1);
            }
        }

        public void StartGPS()
        {
            SetStartGPSButtonActive(false);

            StartCoroutine(CheckGPSSupport());
        }

        public void UpdateGPS() 
        {
            CheckGPS();
        }
        public void StopGPS()
        {
            SetStartGPSButtonActive(true);
            StopCoroutine(CheckGPSSupport());
        }

        void OnEnable()
        {

            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                // The user authorized use of the FineLocation.
            }
            else
            {
                // We do not have permission to use the FineLocation.
                // Ask for permission or proceed without the functionality enabled.
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            //

        }
    }
}
