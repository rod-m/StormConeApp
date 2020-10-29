
using UnityEngine;

public class ParkInfoToggle : MonoBehaviour
{
   public void ShowInfo(GameObject activate)
   {
      activate.SetActive(true);
    
   }

   public void HideObject(GameObject deactivate)
   {
      deactivate.SetActive(false);
   }
}
