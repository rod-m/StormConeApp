using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private AbstractMap map;
    private Locations locations;
   

    private void Start()
    {
         locations= new Locations();
        locations.InitializeTable();
    }
    
    public void SwitchPark(string park)
    {
        
        map.Initialize((Vector2d) locations.parks[park],16);
//        Debug.Log("map changed to" + "   " + map.Options.locationOptions.latitudeLongitude.ToString());
    }
}
