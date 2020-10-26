using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraPanning : MonoBehaviour
{
    [SerializeField] private float height;

    public int speed = 4;
    private Camera camera;
    public float MINSCALE = 2.0F; 
    public float MAXSCALE = 5.0F; 
    public float minPinchSpeed = 5.0F; 
    public float varianceInDistances = 5.0F; 
    private float touchDelta = 0.0F; 
    private Vector2 prevDist = new Vector2(0,0); 
    private Vector2 curDist = new Vector2(0,0); 
    private float speedTouch0 = 0.0F; 
    private float speedTouch1 = 0.0F;
    

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        Input.multiTouchEnabled = true;

        height = this.transform.position.y;
    }

    // Update is called once per frame
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
}
