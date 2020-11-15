using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;

public class MapCameraPanning : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform interestAreaTransform;
    private DistanceChecker distChecker;
    private bool isSliding;

    void Start()
    {
        distChecker = FindObjectOfType<DistanceChecker>();
        PointAt(playerTransform.transform.position.x, playerTransform.position.z);
    }
    
    void Update()
    {
       // PinchZoom();
       if (distChecker.CanSlide())
       {
           //slide camera
           if (!distChecker.PlayerHasMoved())
           {
               SlideCamera();
           }

           PointAt(playerTransform.transform.position.x, playerTransform.position.z);
           
       }
       else
       {
           PointAt(interestAreaTransform.position.x, interestAreaTransform.position.z);
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
    }
    

    private void PointAt(float targetX, float targetZ)
    {
        transform.position = new Vector3(targetX, 130, targetZ);
    }
}
    

