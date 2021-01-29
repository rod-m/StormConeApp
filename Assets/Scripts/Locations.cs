using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;

public class Locations 
{
   public static Vector2d PeelPark = new Vector2d(53.486786, -2.271412);
   public static Vector2d BulieHill = new Vector2d(53.490445, -2.306705);
   public static Vector2d BridgeWater = new Vector2d(53.500633, -2.399245);
   public static Vector2d Chalkwell = new Vector2d(51.544578, 0.676641);
   
   public Hashtable parks = new Hashtable();

   public void InitializeTable()
   {
      parks.Add("peel park", PeelPark);
      parks.Add("bulie hill", BulieHill);
      parks.Add("bridgewater", BridgeWater);
      parks.Add("chalkwell", Chalkwell);
   }
}
