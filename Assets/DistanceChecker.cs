using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Factories;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Camera camera;

    private float distance;
    private float prevDistance;
    private bool canSlideCamera;
    private bool hasMoved;
    private bool shouldDisableNav;
    
    // Start is called before the first frame update
    void Start()
    {
        canSlideCamera = true;
        shouldDisableNav = false;
    }

    private void Update()
    {
    
        distance = Vector3.Distance(target.transform.position, this.transform.position);
        prevDistance = distance;

        if (Mathf.Abs(distance - prevDistance) > 0)
        {
            hasMoved = true;
        }
        canSlideCamera = true;
        if (distance <= 25)
        {
            Debug.Log("Entering the area...");
            canSlideCamera = false;
        }
        
    }

    public bool CanSlide() => canSlideCamera;

    public bool PlayerHasMoved() => hasMoved;

  
    
}
