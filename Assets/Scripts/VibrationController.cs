using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    // Start is called before the first frame update
  
   

    void Start(){
      
     
        StartCoroutine(VibrateDuration());

    }

    public IEnumerator VibrateDuration()

    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.5f);
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.5f);
        Handheld.Vibrate();
    }


}
