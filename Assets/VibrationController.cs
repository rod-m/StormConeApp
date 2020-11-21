using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    // Start is called before the first frame update
  
   

    void Start(){
        Debug.Log("Vibrated!?");
        StartCoroutine(VibrateDuration());

    }

    public IEnumerator VibrateDuration()

    {

        yield return new WaitForSeconds(1.0f);
        Handheld.Vibrate();
        yield return new WaitForSeconds(1.0f);
        Handheld.Vibrate();
        yield return new WaitForSeconds(1.0f);
        Handheld.Vibrate();
    }


}
