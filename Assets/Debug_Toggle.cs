using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_Toggle : MonoBehaviour
{

    public List<RectTransform> UIDebug;
    private bool changePos;
    // Start is called before the first frame update
    void Start()
    {
        changePos = false;
    }

    private void offScreen() 
    {
        foreach (RectTransform r in UIDebug) 
        {
            r.position = new Vector3(r.position.x + 5000, r.position.y, r.position.z);
        }
        changePos = true;
    }

    private void onScreen() 
    {

        foreach (RectTransform r in UIDebug)
        {
            r.position = new Vector3(r.position.x - 5000, r.position.y, r.position.z);
        }
        changePos = false;
    }

    public void toggleDebug() 
    {
        if (!changePos)
            offScreen();
        else
            onScreen();
    }
}
