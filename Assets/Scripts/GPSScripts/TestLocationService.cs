using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

[RequireComponent(typeof(Canvasmanager))]
public class TestLocationService : MonoBehaviour
{

    [SerializeField]
    Button m_StartGPSButton;
    [SerializeField]
    Button m_PlaceAgainButton;


    private Canvasmanager canvasmanager;

    private float GPSX;
    private float GPSZ;

    public bool test;
    public GameObject player;
    public GameObject soundSource;
    private List<GameObject> placeObjects = new List<GameObject>();
    private void Start()
    {
        canvasmanager = GetComponent<Canvasmanager>();

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
    
    void SetPlaceAgainActive(bool active)
    {
        if (m_PlaceAgainButton != null)
            m_PlaceAgainButton.gameObject.SetActive(active);
    }

    

    IEnumerator CheckGPSSupport()
    {
        canvasmanager.m_LogText.text = "Init GPS!";
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
        
            canvasmanager.Log("*** GPS Not Enabled By USER! ***");
            yield return new WaitForSeconds(3);
            canvasmanager.m_LocationText.text = "NO GPS";
            canvasmanager.m_LogText.text = "";

            yield break;
        }


        // Start service before querying location
        Input.location.Start(2, 2);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            //Debug.Log("Waiting " + maxWait);
            canvasmanager.m_LocationText.text = "Waiting... " + maxWait;
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            canvasmanager.Log("Timed out");
            canvasmanager.m_LocationText.text = "GSP Timeout 20s!";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            canvasmanager.Log("Unable to determine device location");
            canvasmanager.m_LocationText.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            canvasmanager.Log( Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously

        // Input.location.Stop();
        Input.compass.enabled = true;
        StartCoroutine(CheckGPS());
    }

    public void PlaceAgain()
    {
        test = true;
    }
    IEnumerator CheckGPS()
    {
        while (true)
        {
            if (test)
            {
                if (placeObjects.Count > 0)
                {
                    foreach (var gm in placeObjects)
                    {
                        Destroy(gm);
                    }
                    placeObjects.Clear();
                }
                SetPlaceAgainActive(false);
                canvasmanager.m_LocationText.text = "Adding Location Marker";
                yield return new WaitForSeconds(3f);
                var g = Instantiate(soundSource);
                g.name = "sound source";
                placeObjects.Add(g);
                yield return new WaitForSeconds(0.1f);
                GameObject p = Instantiate(player);
                placeObjects.Add(p);
                test = false;
                canvasmanager.m_LocationText.text = $"Added Location Marker {Input.location.lastData.latitude} {Input.location.lastData.longitude}";
                                      
                yield return new WaitForSeconds(2f);
                SetPlaceAgainActive(true);
            }
            GPSX = Input.location.lastData.latitude;
            GPSZ = Input.location.lastData.longitude;
            canvasmanager.m_LocationText.text = $"Lon: {GPSZ} Lat: {GPSX}\n Accuracy: {Input.location.lastData.horizontalAccuracy}m";
                                 
            //              " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartGPS()
    {
        // allow me to place again without force close app
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
        SetPlaceAgainActive(false);
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
