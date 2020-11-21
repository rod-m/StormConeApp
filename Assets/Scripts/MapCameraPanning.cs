using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class MapCameraPanning : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform interestAreaTransform;

    [SerializeField] private GameObject leavingPanel;
    [SerializeField] private GameObject CenterCameraToggle;
    private DistanceChecker distChecker;
    private bool isSliding;
    private float speed = 0.025f;

    private bool _shouldCenterCamera;
    private GameObject directions;

    private DirectionsFactory _directionsFactory;
    void Start()
    {
        OnToggleValueChanged(true);
        _directionsFactory = FindObjectOfType<DirectionsFactory>();
        distChecker = FindObjectOfType<DistanceChecker>();

        CenterCamera(playerTransform.position.x, playerTransform.position.z);
    }
    
    void Update()
    {
        
        SlideCamera();
       
       if(distChecker.UserIsInsideArea())
       {
           CenterCamera(interestAreaTransform.position.x, interestAreaTransform.position.z);
           DisableNavigationUI();
           DisableNavigation();
           CheckIfTheUserIsLeaving();
           _shouldCenterCamera = false;
       }

        else if (_shouldCenterCamera && !distChecker.UserIsInsideArea())
        {
            var position = playerTransform.position;
            CenterCamera(position.x,position.z);
        }

    }

    /*private void PinchZoom()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
        {
            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
            prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
            touchDelta = curDist.magnitude - prevDist.magnitude;
            speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
            speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
            if ((touchDelta + varianceInDistances <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
            {
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView + (1 * speed),40,60);
            }
            if ((touchDelta +varianceInDistances > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
            {
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView - (1 * speed),40,60);
            }
        }
    }
    
    //Camera positioning

    private void ZoomMap()
    {
        while (zoom < 16)
        {
            zoom++;
            map.SetZoom(zoom);
        }
    }*/

    private void SlideCamera()
    {
        //slide the camera left and right
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
    }

    
    //function for a button to recentrate the camera on yourself
    public void OnToggleValueChanged(bool centerCamera)
    {
        _shouldCenterCamera = centerCamera;
    }


    private void CenterCamera(float x, float z)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, x, Time.deltaTime * 2), 130, Mathf.Lerp(transform.position.z, z, Time.deltaTime * 2));
    }
    
    private void DisableNavigation(){
        _directionsFactory._waypoints = new Transform[0];
        directions = GameObject.Find("directions"); 
        if(directions){ Destroy(directions);}  
    }

    private void CheckIfTheUserIsLeaving()
    {
        if (distChecker.UserIsOutsideArea())
        {
            leavingPanel.SetActive(true);
            
        }
    }

    private void DisableNavigationUI()
    {
        leavingPanel.SetActive(false);
        CenterCameraToggle.SetActive(false);
    }
}
    

