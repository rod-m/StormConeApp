﻿using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using UnityEngine;

public class MapCameraPanning : MonoBehaviour
{

    [HideInInspector]
    public int speed = 4;
    private Camera camera;
    [HideInInspector] public float MINSCALE = 2.0F; 
    [HideInInspector] public float MAXSCALE = 5.0F; 
    [HideInInspector] public float minPinchSpeed = 5.0F; 
    [HideInInspector] public float varianceInDistances = 5.0F; 
    private float touchDelta = 0.0F; 
    private Vector2 prevDist = new Vector2(0,0); 
    private Vector2 curDist = new Vector2(0,0); 
    private float speedTouch0 = 0.0F; 
    private float speedTouch1 = 0.0F;

    private AbstractMap map;
    private float zoom = 10;
    
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        Input.multiTouchEnabled = true;
        map = FindObjectOfType<AbstractMap>();
        map.SetZoom(zoom);
        
        //Get park location
        
        //position the camera at the park coordinates
        
        //zoom map on park 
        ZoomMap();
    }
    
    void Update()
    {
        PinchZoom();
    }

    private void PinchZoom()
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
    }
 
    //Salford parks origin: 53.488097, -2.270823
    
    
}
