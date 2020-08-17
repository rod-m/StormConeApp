//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR; // is this still needed? test
using UnityEngine.XR.ARSubsystems;

//using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    
// ARRaycastManager replaces ARSessionOrigin
// also needs the AR Raycast Manager component addedd to AR Session Origin
    private ARRaycastManager arOrigin; 
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private GameObject singleObjecct = null; 
    private bool placedSingleObjecct = false;
    private bool allowPlacement = true;
    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if(!allowPlacement) return;
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        if (placedSingleObjecct)
        {
            singleObjecct.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placedSingleObjecct = true;
            singleObjecct = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        }
        
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        if (Camera.current == null) return;
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);
       
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
